using Game.Services.Sound.Pitch.Interfaces;
using UnityEngine;

namespace Game.Services.Sound.Pitch
{
    public class PitchService : IPitchService
    {
        protected PitchService(float value, float step, float time, int stepsCount)
        {
            _value = value;
            _step = step;
            _time = time;
            
            _stepsCount = stepsCount;
        }

        protected PitchService(IPitchSettings settings) 
            : this(settings.Value, settings.Step, settings.Time, settings.StepsCount)
        {
            
        }
        
        private readonly float _value;
        private readonly float _step;
        private readonly float _time;
        
        private readonly int _stepsCount;

        private float _lastTime;
        private int _currentStep;

        private bool _isNotFirst;

        public float GetCurrentPitch()
        {
            var pitch = _value;
            if (_isNotFirst)
            {
                var delta = _lastTime - Time.time;
                if (delta <= _time)
                {
                    pitch = _value + _step * _currentStep;
                }
                else
                {
                    _currentStep = 0;
                }
            }

            _lastTime = Time.time;
            _currentStep = (_currentStep + 1) % _stepsCount;

            return pitch;
        }
    }
}