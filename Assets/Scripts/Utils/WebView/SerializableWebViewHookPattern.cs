using System;
using UnityEngine;
using Utils.WebView.Interfaces;

namespace Utils.WebView
{
    [Serializable]
    public struct SerializableWebViewHookPattern : IWebViewHookPattern
    {
        [field: SerializeField]
        public string Allow
        {
            get;
            private set;
        }

        [field: SerializeField]
        public string Deny
        {
            get;
            private set;
        }

        [field: SerializeField]
        public string Hook
        {
            get;
            private set;
        }
    }
}