using System;
using Game.UI.Counter.Interfaces;

namespace Game.UI.Counter
{
    public class CounterModel : ICounterModel
    {
        public event Action<int> OnValueChanged = _ => { };
        
        private int _count = 0;
        
        public void Increment()
        {
            _count++;
            OnValueChanged.Invoke(_count);
        }
    }
}