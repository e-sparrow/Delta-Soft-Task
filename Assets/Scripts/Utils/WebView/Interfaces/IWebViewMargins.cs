namespace Utils.WebView.Interfaces
{
    public interface IWebViewMargins
    {
        int Left
        {
            get;
        }

        int Top
        {
            get;
        }

        int Right
        {
            get;
        }

        int Bottom
        {
            get;
        }
        
        bool IsRelative
        {
            get;
        }
    }
}