using System;
using Game.Core.Ball.Interfaces;
using UnityEngine;

namespace Game.Core.Ball
{
    [Serializable]
    public class SerializableBallModel : IBallModel
    {
        [field: SerializeField]
        public float JumpForce
        {
            get;
            private set;
        }

        [field: SerializeField]
        public float Speed
        {
            get;
            private set;
        }
    }
}