using System;
using UnityEngine;
using Utils.WebView.Interfaces;

namespace Utils.WebView
{
    [Serializable]
    public struct SerializableWebViewBasicAuthInfo : IWebViewBasicAuthInfo
    {
        [field: SerializeField]
        public string Id
        {
            get;
            private set;
        }

        [field: SerializeField]
        public string Password
        {
            get;
            private set;
        }
    }
}