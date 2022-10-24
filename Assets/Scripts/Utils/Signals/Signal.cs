using System;
using Utils.Signals.Interfaces;

namespace Utils.Signals
{
    public class Signal : ISignal
    {
        public event Action OnInvoke = () => { };
        
        public void Invoke()
        {
            OnInvoke.Invoke();
        }
    }
}