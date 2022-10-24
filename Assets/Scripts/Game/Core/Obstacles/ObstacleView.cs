using Game.Core.Obstacles.Interfaces;
using UnityEngine;

namespace Game.Core.Obstacles
{
    public class ObstacleView : MonoBehaviour, IObstacleView
    {
        [SerializeField] private SpriteRenderer lowerSprite;
        [SerializeField] private SpriteRenderer topSprite;

        [SerializeField] private BoxCollider2D lowerCollider;
        [SerializeField] private BoxCollider2D topCollider;
        
        public void SetThickness(float value)
        {
            var lowerScale = lowerSprite.size;
            lowerScale.x = value;

            lowerSprite.size = lowerScale;
            lowerCollider.size = lowerScale;

            var topScale = topSprite.size;
            topScale.x = value;

            topSprite.size = topScale;
            topCollider.size = topScale;
        }

        public void SetGap(float value)
        {
            var lowerY = -lowerSprite.size.y / 2 - value;
            lowerSprite.transform.localPosition = Vector3.up * lowerY;

            var topY = topSprite.size.y / 2 + value;
            topSprite.transform.localPosition = Vector3.up * topY;
        }

        public void SetOffset(float offset)
        {
            var position = transform.position;
            position.y = offset;

            transform.position = position;
        }

        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }
    }
}
