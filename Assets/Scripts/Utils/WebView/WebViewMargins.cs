using Utils.WebView.Interfaces;

namespace Utils.WebView
{
    public readonly struct WebViewMargins : IWebViewMargins
    {
        public WebViewMargins
        (
            int left = 0, 
            int top = 0, 
            int right = 0, 
            int bottom = 0,
            bool isRelative = false
        )
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
            
            IsRelative = isRelative;
        }

        public int Left
        {
            get;
        }

        public int Top
        {
            get;
        }

        public int Right
        {
            get;
        }

        public int Bottom
        {
            get;
        }

        public bool IsRelative
        {
            get;
        }
    }
}