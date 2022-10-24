using UnityEngine;

namespace Game.Core.Obstacles.Interfaces
{
    public interface IObstacleSettings
    {
        ObstacleView Prefab
        {
            get;
        }

        Vector2 StartPoint
        {
            get;
        }
        
        float Thickness
        {
            get;
        }

        float Gap
        {
            get;
        }

        float Range
        {
            get;
        }

        float MinOffsetDifference
        {
            get;
        }

        float MaxOffsetDifference
        {
            get;
        }
    }
}