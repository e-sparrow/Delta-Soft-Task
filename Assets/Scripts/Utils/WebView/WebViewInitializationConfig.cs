using Utils.WebView.Enums;
using Utils.WebView.Interfaces;

namespace Utils.WebView
{
    public readonly struct WebViewInitializationConfig : IWebViewInitializationConfig
    {
        public WebViewInitializationConfig
        (
                bool useIFrame, 
                bool alertDialogEnabled, 
                bool cameraAccessRequired,
                bool microphoneAccessRequired,
                bool scrollbarsVisibility,
                int textScale, 
                EWebViewRequestType requestType, 
                
                IWebViewHookPattern hookPattern = null, 
                IWebViewBasicAuthInfo basicAuthInfo = null, 
                IWebViewMargins margins = null
        )
        {
            UseIFrame = useIFrame;
            AlertDialogEnabled = alertDialogEnabled;
            CameraAccessRequired = cameraAccessRequired;
            MicrophoneAccessRequired = microphoneAccessRequired;
            ScrollbarsVisibility = scrollbarsVisibility;
            TextScale = textScale;
            RequestType = requestType;

            HasHookPattern = hookPattern != null;
            HookPattern = hookPattern;
            
            HasBasicAuthInfo = basicAuthInfo != null;
            BasicAuthInfo = basicAuthInfo;
            
            Margins = margins ?? new WebViewMargins();
        }

        public bool UseIFrame
        {
            get;
        }

        public bool AlertDialogEnabled
        {
            get;
        }

        public bool CameraAccessRequired
        {
            get;
        }

        public bool MicrophoneAccessRequired
        {
            get;
        }

        public bool ScrollbarsVisibility
        {
            get;
        }

        public EWebViewRequestType RequestType
        {
            get;
        }

        public int TextScale
        {
            get;
        }

        public bool HasHookPattern
        {
            get;
        }

        public IWebViewHookPattern HookPattern
        {
            get;
        }

        public bool HasBasicAuthInfo
        {
            get;
        }

        public IWebViewBasicAuthInfo BasicAuthInfo
        {
            get;
        }

        public IWebViewMargins Margins
        {
            get;
        }
    }
}