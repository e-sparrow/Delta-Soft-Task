using System;
using Game.Services.Sound.Enums;
using UnityEngine;

namespace Game.Services.Sound
{
    [Serializable]
    public struct SoundSource
    {
        [field: SerializeField]
        public ESoundType Type
        {
            get;
            private set;
        }

        [field: SerializeField]
        public AudioClip Clip
        {
            get;
            private set;
        }
    }
}