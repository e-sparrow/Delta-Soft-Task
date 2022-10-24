using Game.Core.Interfaces;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Core.Level
{
    public class LevelPresenter : IInitializable, ILateDisposable
    {
        public LevelPresenter(IGameLoop gameLoop)
        {
            _gameLoop = gameLoop;
        }

        private const int SceneIndex = 0;

        private readonly IGameLoop _gameLoop;
        
        public void Initialize()
        {
            _gameLoop.GameOver.OnInvoke += GameOver;
        }

        public void LateDispose()
        {
            _gameLoop.GameOver.OnInvoke -= GameOver;
        }

        private void GameOver()
        {
            SceneManager.LoadScene(SceneIndex);
        }
    }
}