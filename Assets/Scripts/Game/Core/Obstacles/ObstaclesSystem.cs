using Game.Core.Interfaces;
using Game.Core.Obstacles.Interfaces;
using Zenject;

namespace Game.Core.Obstacles
{
    public class ObstaclesSystem : IObstacleSystem, IInitializable, ILateDisposable
    {
        public ObstaclesSystem(IGameLoop gameLoop, ObstaclePool pool)
        {
            _gameLoop = gameLoop;
            _pool = pool;
        }

        private const int StartCount = 2;

        private readonly IGameLoop _gameLoop;
        private readonly ObstaclePool _pool;

        public void Initialize()
        {
            _gameLoop.LevelLoaded.OnInvoke += Start;
            _gameLoop.GameOver.OnInvoke += Clear;
        }

        public void LateDispose()
        {
            _gameLoop.LevelLoaded.OnInvoke -= Start;
            _gameLoop.GameOver.OnInvoke -= Clear;
        }

        public void Update()
        {
            _pool.Spawn();
        }

        private void Start()
        {
            for (var i = 0; i < StartCount; i++)
            {
                _pool.Spawn();
            }
        }

        private void Clear()
        {
            _pool.Clear();
        }
    }
}