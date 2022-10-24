using Game.Core.Interfaces;
using UnityEngine;
using Utils.Signals;
using Utils.Signals.Interfaces;

namespace Game.Core
{
    public class MonoGameLoop : MonoBehaviour, IGameLoop
    {
        public ISignal LevelLoaded
        {
            get;
        } = new Signal();

        public ISignal TimerOver
        {
            get;
        } = new Signal();

        public ISignal GameOver
        {
            get;
        } = new Signal();

        private void Start()
        {
            LevelLoaded.Invoke();
        }
    }
}