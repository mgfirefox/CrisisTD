using Cinemachine;
using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class VirtualCameraView : AbstractView, IVirtualCameraView
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private CinemachineVirtualCamera virtualCamera;

        [SerializeField]
        [BoxGroup("Transform")]
        [ReadOnly]
        private Vector3 position;
        [SerializeField]
        [BoxGroup("Transform")]
        [ReadOnly]
        private Quaternion orientation;

        [SerializeField]
        [BoxGroup("VirtualCamera")]
        [ReadOnly]
        private int priority;

        public Vector3 Position
        {
            get => position;
            set
            {
                position = value;

                if (IsDestroyed)
                {
                    return;
                }

                Transform.position = position;
            }
        }
        public Quaternion Orientation
        {
            get => orientation;
            set
            {
                orientation = value;

                if (IsDestroyed)
                {
                    return;
                }

                Transform.rotation = orientation;
            }
        }

        public int Priority
        {
            get => priority;
            set
            {
                priority = value;

                if (IsDestroyed)
                {
                    return;
                }

                virtualCamera.Priority = priority;
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            position = Transform.position;
            orientation = Transform.rotation;
            priority = virtualCamera.Priority;
        }
    }
}
