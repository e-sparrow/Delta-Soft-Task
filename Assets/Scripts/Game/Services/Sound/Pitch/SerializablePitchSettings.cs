using System;
using Game.Services.Sound.Pitch.Interfaces;
using UnityEngine;

namespace Game.Services.Sound.Pitch
{
    [Serializable]
    public class SerializablePitchSettings : IPitchSettings
    {
        [field: SerializeField]
        public float Value
        {
            get; 
            private set;
        }

        [field: SerializeField]
        public float Step
        {
            get; 
            private set;
        }

        [field: SerializeField]
        public float Time
        {
            get;
            private set;
        }

        [field: SerializeField]
        public int StepsCount
        {
            get; 
            private set;
        }
    }
}