using System;
using Game.UI.Timer.Interfaces;
using UnityEngine;

namespace Game.UI.Timer
{
    [Serializable]
    public class SerializableTimerModel : ITimerModel
    {
        [field: SerializeField]
        public float TotalTime
        {
            get;
            private set;
        }
    }
}