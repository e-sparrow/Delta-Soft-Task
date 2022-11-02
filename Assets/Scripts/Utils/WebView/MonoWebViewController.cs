using UnityEngine;
using Utils.WebView.Interfaces;

namespace Utils.WebView
{
    [AddComponentMenu("ESparrow/MonoWebViewController")]
    public class MonoWebViewController : MonoBehaviour, IWebViewController
    {
        [SerializeField] private SerializableWebViewInitializationConfig config;

        [SerializeField] private WebViewObject webViewObject;

        private IWebViewController _innerController;
        private bool _isInitialized = false;

        private void Awake()
        {
            _isInitialized = true;
            _innerController = new WebViewController(config, webViewObject);
        }

        public void OpenUrl(string url)
        {
            if (!_isInitialized)
            {
                var message = $@"Trying to open url {url} before initialized controller!";
                throw new WebViewException(message);
            }

            _innerController.OpenUrl(url);
        }
    }
}