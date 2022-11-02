using System;
using System.Text;

namespace Utils.WebView
{
    public class WebViewException : Exception
    {
        public WebViewException(string message)
        {
            _message = message;
        }

        private const string Tag = "<b><color=red>WebViewController throws:</color></b> ";

        private readonly string _message;
        
        public override string Message => FormatMessage(_message);

        private static string FormatMessage(string message)
        {
            var builder = new StringBuilder();
            
            var result = builder
                .Append(Tag)
                .Append(message)
                .ToString();

            return result;
        }
    }
}