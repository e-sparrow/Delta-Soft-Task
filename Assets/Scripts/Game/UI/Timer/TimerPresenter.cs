using Game.Core.Interfaces;
using Game.UI.Timer.Interfaces;
using Input;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.UI.Timer
{
    public class TimerPresenter : IInitializable, ILateDisposable
    {
        public TimerPresenter(IGameLoop gameLoop, ITimerModel model, ITimerView view)
        {
            _gameLoop = gameLoop;
            
            _model = model;
            _view = view;
        }

        private readonly IGameLoop _gameLoop;
        
        private readonly ITimerModel _model;
        private readonly ITimerView _view;

        private Controls _controls;
        
        public void Initialize()
        {
            _controls = new Controls();
            _controls.Enable();
            
            _gameLoop.LevelLoaded.OnInvoke += StartTimer;
        }

        public void LateDispose()
        {
            _gameLoop.LevelLoaded.OnInvoke += StartTimer;
        }

        private void StartTimer()
        {
            _view.SetValue(_model.TotalTime);
            
            _controls.Main.Jump.performed += Click;

            void Click(InputAction.CallbackContext context)
            {
                _controls.Main.Jump.performed -= Click;
                
                _view.StartTimer(_model.TotalTime);
                _view.OnTimerOver += Perform;

                void Perform()
                {
                    _view.OnTimerOver -= Perform;
                    _gameLoop.TimerOver.Invoke();
                }
            }
        }
    }
}