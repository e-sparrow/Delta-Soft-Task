using System;
using System.Collections.Generic;
using Game.Services.Sound.Interfaces;
using UnityEngine;

namespace Game.Services.Sound
{
    [Serializable]
    public class SerializableSoundSettings : ISoundSettings
    {
        [NonReorderable]
        [SerializeField] private SoundSource[] sources;

        public IEnumerable<SoundSource> Sources => sources;
    }
}