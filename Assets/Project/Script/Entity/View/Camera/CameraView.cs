using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class CameraView : AbstractView, ICameraView
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private new Camera camera;

        [SerializeField]
        [BoxGroup("Transform")]
        [ReadOnly]
        private Vector3 position;
        [SerializeField]
        [BoxGroup("Transform")]
        [ReadOnly]
        private Quaternion orientation;

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

        public Ray GetRayFromScreenPosition(Vector3 screenPosition)
        {
            return camera.ScreenPointToRay(screenPosition);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            position = Transform.position;
            orientation = Transform.rotation;
        }
    }
}
