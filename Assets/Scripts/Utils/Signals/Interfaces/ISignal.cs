using System;

namespace Utils.Signals.Interfaces
{
    public interface ISignal
    {
        event Action OnInvoke;

        void Invoke();
    }
}