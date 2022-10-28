using System;
using System.Collections.Generic;
using System.Linq;
using Game.Core.Ball.Interfaces;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Core.Ball
{
    public class BallView : MonoBehaviour, IBallView
    {
        public event Action OnNeckHit = () => { };
        public event Action OnBallCollide = () => { };
        public event Action OnPassObstacle = () => { };

        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int GameOver = Animator.StringToHash("GameOver");
        
        [SerializeField] private Animator animator;
        [SerializeField] private Rigidbody2D body;

        private readonly Queue<float> _forces = new Queue<float>();

        private float _speed = 0f;
        private bool _isActive = false;

        public void SetActive(bool active)
        {
            body.simulated = active;
            _isActive = active;
        }

        public void PushUp(float force)
        {
            animator.SetTrigger(Hit);
            _forces.Enqueue(force);
        }

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }
        
        public void OnGameOver()
        {
            animator.SetTrigger(GameOver);
        }

        [UsedImplicitly]
        public void NeckHit()
        {
            if (_forces.Any())
            {
                var force = _forces.Dequeue();
                var value = Vector2.up * force;
                body.AddForce(value, ForceMode2D.Impulse);

                if (_forces.Any())
                {
                    _forces.Dequeue();
                }
            }
            
            OnNeckHit.Invoke();
        }

        private void FixedUpdate()
        {
            if (!_isActive) return;
            
            var force = Vector2.right * _speed * Time.fixedDeltaTime;
            body.AddForce(force, ForceMode2D.Force);
        }

        private void OnCollisionEnter2D()
        {
            OnBallCollide.Invoke();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            OnPassObstacle.Invoke();
        }

        private void OnBecameInvisible()
        {
            OnBallCollide.Invoke();
        }
    }
}