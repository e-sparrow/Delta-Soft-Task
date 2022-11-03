using System.Collections;
using UnityEngine;
using Utils.WebView.Interfaces;

namespace Utils.WebView
{
    [AddComponentMenu("ESparrow/MonoWebViewController")]
    public class MonoWebViewController : MonoBehaviour, IWebViewController
    {
        [SerializeField] private SerializableWebViewInitializationConfig config;
        [SerializeField] private bool enableBackButton;
        
        [SerializeField] private WebViewObject webViewObject;

        private IWebViewController _innerController;
        private bool _isInitialized = false;

        private Coroutine _lifetimeCoroutine;

        private void Awake()
        {
            _isInitialized = true;
            _innerController = new WebViewController(config, webViewObject);
            
            DontDestroyOnLoad(this);
        }

        public void OpenUrl(string url)
        {
            if (!_isInitialized)
            {
                var message = $@"Trying to open url {url} before initialized controller!";
                throw new WebViewException(message);
            }

            _innerController.OpenUrl(url);

            if (_lifetimeCoroutine != null)
            {
                StopCoroutine(_lifetimeCoroutine);
            }
            
            _lifetimeCoroutine = StartCoroutine(WebViewLifetimeCoroutine());
        }

        public void GoBack()
        {
            _innerController.GoBack();
        }

        public void GoForward()
        {
            _innerController.GoForward();
        }

        private IEnumerator WebViewLifetimeCoroutine()
        {
            while (true)
            {
                if (enableBackButton && UnityEngine.Input.GetKeyDown(KeyCode.Escape))
                {
                    _innerController.GoBack();    
                }
                
                yield return null;
            }
        }
    }
}