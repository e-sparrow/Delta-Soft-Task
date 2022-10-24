using System;

namespace Game.UI.Timer.Interfaces
{
    public interface ITimerView
    {
        event Action OnTimerOver;

        void SetValue(float value);
        void StartTimer(float time);
    }
}