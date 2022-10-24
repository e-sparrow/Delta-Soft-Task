using System;
using Game.Core.Obstacles.Interfaces;

namespace Game.Core.Ball.Interfaces
{
    public interface IBallView
    {
        event Action OnNeckHit;
        event Action OnBallCollide;
        event Action OnPassObstacle;

        void SetActive(bool active);
        void PushUp(float force);
        void SetSpeed(float speed);

        void OnGameOver();
    }
}