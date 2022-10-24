using Game.Constants;
using Game.Core;
using Game.Core.Ball;
using Game.Core.Level;
using Game.Core.Obstacles;
using Game.Core.Obstacles.Interfaces;
using Game.UI.Counter;
using Game.UI.Timer;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class LevelInstaller : MonoInstaller<LevelInstaller>
    {
        [SerializeField] private SerializableObstacleSettings obstacleSettings;
            
        [SerializeField] private SerializableBallModel ballModel;
        [SerializeField] private SerializableTimerModel timerModel;
        
        [SerializeField] private MonoGameLoop gameLoop;

        [SerializeField] private BallView ballView;
        [SerializeField] private TimerView timerView;
        [SerializeField] private CounterView counterView;
        
        [SerializeField] private AudioSource audioSourcePrefab;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<MonoGameLoop>()
                .FromInstance(gameLoop)
                .AsSingle();

            Container
                .BindInterfacesTo<LevelPresenter>()
                .AsSingle()
                .NonLazy();

            BindBall();
            BindTimer();
            BindCounter();
            BindObstacles();
            
            Container
                .BindMemoryPool<AudioSource, MemoryPool<AudioSource>>()
                .WithInitialSize(8)
                .WithMaxSize(16)
                .FromComponentInNewPrefab(audioSourcePrefab)
                .UnderTransformGroup(ProjectConstants.AudioPoolTransformGroup);
        }

        private void BindTimer()
        {
            Container
                .BindInterfacesTo<SerializableTimerModel>()
                .FromInstance(timerModel)
                .AsSingle();
            
            Container
                .BindInterfacesTo<TimerView>()
                .FromInstance(timerView)
                .AsSingle();

            Container
                .BindInterfacesTo<TimerPresenter>()
                .AsSingle()
                .NonLazy();
        }

        private void BindCounter()
        {
            Container
                .BindInterfacesTo<CounterModel>()
                .AsSingle();
            
            Container
                .BindInterfacesTo<CounterView>()
                .FromInstance(counterView)
                .AsSingle();

            Container
                .BindInterfacesTo<CounterPresenter>()
                .AsSingle()
                .NonLazy();
        }

        private void BindBall()
        {
            Container
                .BindInterfacesTo<SerializableBallModel>()
                .FromInstance(ballModel)
                .AsSingle();
            
            Container
                .BindInterfacesTo<BallView>()
                .FromInstance(ballView)
                .AsSingle();
            
            Container
                .BindInterfacesTo<BallPresenter>()
                .AsSingle()
                .NonLazy();
        }

        private void BindObstacles()
        {
            Container
                .BindInterfacesTo<SerializableObstacleSettings>()
                .FromInstance(obstacleSettings)
                .AsSingle();
            
            Container
                .BindMemoryPool<ObstacleView, ObstaclePool>()
                .WithInitialSize(2)
                .WithMaxSize(4)
                .FromComponentInNewPrefab(obstacleSettings.Prefab)
                .UnderTransformGroup(ProjectConstants.ObstaclePoolTransformGroup);
            
            Container
                .BindInterfacesTo<ObstaclesSystem>()
                .AsSingle()
                .NonLazy();
        }
    }
}
