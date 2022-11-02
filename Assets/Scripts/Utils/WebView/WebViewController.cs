using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Utils.WebView.Enums;
using Utils.WebView.Interfaces;
using Zenject;
using Object = UnityEngine.Object;

namespace Utils.WebView
{
    public class WebViewController : IWebViewController, IInitializable
    {
        public WebViewController(IWebViewInitializationConfig config, WebViewObject instance = null)
        {
            _config = config;

            _hasInstance = instance != null;
            _instance = instance;

            _webViewObject = new Lazy<WebViewObject>(GetWebViewObject);
        }
        
        private const string WebViewObjectName = "WebViewObject";
        
        private readonly IWebViewInitializationConfig _config;
        
        private readonly bool _hasInstance;
        private readonly WebViewObject _instance;

        private readonly Lazy<WebViewObject> _webViewObject;

        private bool _isInitialized = false;

        #region JSCode

        private const string UseIFrameLoadedJS = @"
                  if (window && window.webkit && window.webkit.messageHandlers && window.webkit.messageHandlers.unityControl) {
                    window.Unity = {
                      call: function(msg) {
                        window.webkit.messageHandlers.unityControl.postMessage(msg);
                      }
                    }
                  } else {
                    window.Unity = {
                      call: function(msg) {
                        var iframe = document.createElement('IFRAME');
                        iframe.setAttribute('src', 'unity:' + msg);
                        document.documentElement.appendChild(iframe);
                        iframe.parentNode.removeChild(iframe);
                        iframe = null;
                      }
                    }
                  }
                ";

        private const string DefaultLoadedJS = @"
                  if (window && window.webkit && window.webkit.messageHandlers && window.webkit.messageHandlers.unityControl) {
                    window.Unity = {
                      call: function(msg) {
                        window.webkit.messageHandlers.unityControl.postMessage(msg);
                      }
                    }
                  } else {
                    window.Unity = {
                      call: function(msg) {
                        window.location = 'unity:' + msg;
                      }
                    }
                  }
                ";

        private const string WebPlayerLoadedJS = "window.Unity = {" +
                                                 "   call:function(msg) {" +
                                                 "       parent.unityWebView.sendMessage('WebViewObject', msg)" +
                                                 "   }" +
                                                 "};";

        private const string NavigatorUserAgentJS = @"Unity.call('ua=' + navigator.userAgent)";
        
        #endregion
        
        public void Initialize()
        {
            _webViewObject.Value.Init(
                Callback,
                Error,
                HttpError,
                started: Started,
                hooked: Hooked,
                ld: Loaded
            );
            
            _isInitialized = true;
        }
        
        public void OpenUrl(string url)
        {
            _webViewObject.Value.StopAllCoroutines();
            _webViewObject.Value.StartCoroutine(OpenPageCoroutine(url));
        }

        private WebViewObject GetWebViewObject()
        {
            if (_hasInstance)
            {
                return _instance;
            }

            var find = Object.FindObjectOfType<WebViewObject>();
            if (find != null)
            {
                return find;
            }

            var result = new GameObject(WebViewObjectName)
                .AddComponent<WebViewObject>();

            return result;
        }

        private static string CreateMessageWithPrefix(string prefix, string url)
        {
            var result = $"<b>{prefix}</b>[{url}]";
            return result;
        }

        private static void Callback(string url)
        {
            var message = CreateMessageWithPrefix("CallFromJS", url);
            Debug.Log(message);
        }

        private static void Error(string url)
        {
            var message = CreateMessageWithPrefix("CallOnError", url);
            Debug.LogError(message);
        }

        private static void HttpError(string url)
        {
            var message = CreateMessageWithPrefix("CallOnHttpError", url);
            Debug.LogError(message);
        }
        
        private static void Started(string url)
        {
            var message = CreateMessageWithPrefix("CallOnStarted", url);
            Debug.Log(message);
        }
        
        private static void Hooked(string url)
        {
            var message = CreateMessageWithPrefix("Hooked", url);
            Debug.Log(message);
        }

        private void Loaded(string url)
        {
            Debug.Log(CreateMessageWithPrefix("CallOnLoaded", url));

#if UNITY_EDITOR_OSX || (!UNITY_ANDROID && !UNITY_WEBPLAYER && !UNITY_WEBGL)
                if (_config.UseIFrame)
                {
                    _webViewObject.Value.EvaluateJS(DefaultLoadedJS);
                }
                else
                {
                    _webViewObject.Value.EvaluateJS(UseIFrameLoadedJS);
                }
#elif UNITY_WEBPLAYER || UNITY_WEBGL
                _webViewObject.Value.EvaluateJS(
                    WebPlayerLoadedJS);
#endif
            
            _webViewObject.Value.EvaluateJS(NavigatorUserAgentJS);
        }


        private IEnumerator OpenPageCoroutine(string page)
        {
            if (!_isInitialized)
            {
                Initialize();
            }
            
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
            webViewObject.bitmapRefreshCycle = 1;
#endif
            _webViewObject.Value.SetAlertDialogEnabled(_config.AlertDialogEnabled);
            _webViewObject.Value.SetCameraAccess(_config.CameraAccessRequired);
            _webViewObject.Value.SetMicrophoneAccess(_config.MicrophoneAccessRequired);
            _webViewObject.Value.SetScrollbarsVisibility(_config.ScrollbarsVisibility);
            _webViewObject.Value.SetTextZoom(_config.TextScale); 

            if (_config.HasHookPattern)
            {
                var pattern = _config.HookPattern;
                _webViewObject.Value.SetURLPattern(pattern.Allow, pattern.Deny, pattern.Hook);
            }

            if (_config.HasBasicAuthInfo)
            {
                var authInfo = _config.BasicAuthInfo;
                _webViewObject.Value.SetBasicAuthInfo(authInfo.Id, authInfo.Password);
            }

            var margins = _config.Margins;
            _webViewObject.Value.SetMargins(margins.Left, margins.Top, margins.Right, margins.Bottom, margins.IsRelative);
            
            _webViewObject.Value.SetVisibility(true);

#if !UNITY_WEBPLAYER && !UNITY_WEBGL
            if (page.StartsWith("http"))
            {
                _webViewObject.Value.LoadURL(page.Replace(" ", "%20"));
            }
            else
            {
                var extensions = new string[]
                {
                    ".jpg",
                    ".js",
                    ".html"
                };
                
                foreach (var ext in extensions)
                {
                    var url = page.Replace(".html", ext);
                    
                    var source = System.IO.Path.Combine(Application.streamingAssetsPath, url);
                    var destination = System.IO.Path.Combine(Application.persistentDataPath, url);
                    
                    byte[] result = null;
                    if (source.Contains("://"))
                    {
                        switch (_config.RequestType)
                        {
                            case EWebViewRequestType.UnityWebRequest:
                                var unityWebRequest = UnityWebRequest.Get(source);
                                yield return unityWebRequest.SendWebRequest();
                                result = unityWebRequest.downloadHandler.data;
                                break;
                            
                            case EWebViewRequestType.WWW:
                                var www = new WWW(source);
                                yield return www;
                                result = www.bytes;
                                break;
                        }
                    }
                    else
                    {
                        result = System.IO.File.ReadAllBytes(source);
                    }

                    System.IO.File.WriteAllBytes(destination, result);
                    if (ext == ".html")
                    {
                        _webViewObject.Value.LoadURL("file://" + destination.Replace(" ", "%20"));
                        break;
                    }
                }
            }
#else
        if (Url.StartsWith("http")) {
            _webViewObject.Value.LoadURL(Url.Replace(" ", "%20"));
        } else {
            _webViewObject.Value.LoadURL("StreamingAssets/" + Url.Replace(" ", "%20"));
        }
#endif
            yield break;
        }
    }
}
