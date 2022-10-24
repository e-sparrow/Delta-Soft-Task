using Cinemachine;
using UnityEngine;

namespace Utils.Cinemachine
{
    [SaveDuringPlay]
    public class ClampCameraY : CinemachineExtension
    {   
        [SerializeField] private float minY;
        [SerializeField] private float maxY;
 
        protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage == CinemachineCore.Stage.Body)
            {
                var pos = state.RawPosition;
                
                var y = Mathf.Clamp(pos.y, minY, maxY);
                pos.y = y;
                
                state.RawPosition = pos;
            }
        }
    }
}