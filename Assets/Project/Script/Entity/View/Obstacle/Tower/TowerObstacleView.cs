using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class TowerObstacleView : AbstractView, ITowerObstacleView
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private BasicBoxPhysicalHitboxView boxPhysicalHitbox;

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

        public Vector3 Size
        {
            get => boxPhysicalHitbox.Size;
            set => boxPhysicalHitbox.Size = value;
        }
        public float Length
        {
            get => boxPhysicalHitbox.Length;
            set => boxPhysicalHitbox.Length = value;
        }
        public float Height
        {
            get => boxPhysicalHitbox.Height;
            set => boxPhysicalHitbox.Height = value;
        }
        public float Width
        {
            get => boxPhysicalHitbox.Width;
            set => boxPhysicalHitbox.Width = value;
        }

        public Vector3 GetClosestPosition(Vector3 position)
        {
            return boxPhysicalHitbox.GetClosestPosition(position);
        }

        public bool IsPositionWithin(Vector3 position, float epsilon)
        {
            return boxPhysicalHitbox.IsPositionWithin(position, epsilon);
        }

        public void OnDrawGizmos()
        {
            if (!(Length > 0.0f && Height > 0.0f && Width > 0.0f))
            {
                return;
            }

            Color oldColor = Gizmos.color;
            Matrix4x4 oldMatrix = Gizmos.matrix;

            var scale = new Vector3(1.0f, Constant.epsilon, 1.0f);

            Gizmos.color = Color.red;
            Gizmos.matrix = Matrix4x4.TRS(position, orientation.normalized, scale);

            Gizmos.DrawCube(Vector3.zero, Size);

            Gizmos.color = oldColor;
            Gizmos.matrix = oldMatrix;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            boxPhysicalHitbox.Initialize();
        }

        protected override void OnDestroying()
        {
            boxPhysicalHitbox.Destroy();

            base.OnDestroying();
        }
    }
}
