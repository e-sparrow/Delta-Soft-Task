using Game.Core.Ball.Interfaces;
using Game.Core.Interfaces;
using Game.Core.Obstacles.Interfaces;
using Game.Services.Sound.Enums;
using Game.Services.Sound.Interfaces;
using Game.UI.Counter.Interfaces;
using Input;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.Core.Ball
{
    public class BallPresenter : IInitializable, ILateDisposable
    {
        public BallPresenter
        (
            IBallModel model,
            IBallView view,
            IGameLoop gameLoop,
            ISoundService<ESoundType> soundService,
            ICounterModel counterModel,
            IObstacleSystem obstacleSystem
        )
        {
            _model = model;
            _view = view;
            _gameLoop = gameLoop;
            _soundService = soundService;
            _counterModel = counterModel;
            _obstacleSystem = obstacleSystem;
        }

        private readonly IBallModel _model;
        private readonly IBallView _view;
        private readonly IGameLoop _gameLoop;
        private readonly ISoundService<ESoundType> _soundService;
        private readonly ICounterModel _counterModel;
        private readonly IObstacleSystem _obstacleSystem;

        private Controls _controls;
        
        public void Initialize()
        {
            _controls = new Controls();
            _controls.Enable();

            _gameLoop.TimerOver.OnInvoke += Enable;
        }

        public void LateDispose()
        {
            _controls.Main.Jump.performed -= PerformJump;
            
            _gameLoop.TimerOver.OnInvoke -= Enable;
            
            _view.OnBallCollide -= GameOver;
            _view.OnNeckHit -= NeckHit;
            _view.OnPassObstacle -= PassObstacle;
        }

        private void Enable()
        {
            _controls.Main.Jump.performed += PerformJump;
            
            _view.SetSpeed(_model.Speed);
            _view.SetActive(true);
            
            _view.OnBallCollide += GameOver;
            _view.OnNeckHit += NeckHit;
            _view.OnPassObstacle += PassObstacle;
        }

        private void PerformJump(InputAction.CallbackContext context)
        {
            _view.PushUp(_model.JumpForce);
        }

        private void GameOver()
        {
            _soundService.PlayOneShot(ESoundType.GameOver);
            _gameLoop.GameOver.Invoke();
            
            _view.SetActive(false);
            _view.OnGameOver();
        }
        
        private void NeckHit()
        {
            _soundService.PlayOneShot(ESoundType.NeckHit);
        }

        private void PassObstacle()
        {
            _counterModel.Increment();
            _obstacleSystem.Update();
            
            _soundService.PlayOneShot(ESoundType.Pass);
        }
    }
}