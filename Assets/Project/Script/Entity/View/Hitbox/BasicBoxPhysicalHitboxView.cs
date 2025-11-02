using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class BasicBoxPhysicalHitboxView : AbstractPhysicalHitboxView<BoxCollider>,
        IBasicBoxPhysicalHitboxView
    {
        [SerializeField]
        [BoxGroup("BoxHitbox")]
        [ReadOnly]
        private Vector3 size;

        public Vector3 Size { get => size; set => SetSize(value.x, value.y, value.z); }
        public float Length { get => size.x; set => SetSize(value, size.y, size.z); }
        public float Height { get => size.y; set => SetSize(size.x, value, size.z); }
        public float Width { get => size.z; set => SetSize(size.x, size.y, value); }

        public void SetSize(float length, float height, float width)
        {
            size = new Vector3(length, height, width);

            if (IsDestroyed)
            {
                return;
            }

            Collider.size = size;
        }

        public override Vector3 GetClosestPosition(Vector3 position)
        {
            return Collider.ClosestPoint(position);
        }

        public override bool IsPositionWithin(Vector3 position, float epsilon)
        {
            return GetClosestPosition(position).EqualsApproximately(position, epsilon);
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
            Gizmos.matrix = Matrix4x4.TRS(Position, Quaternion.identity, scale);

            Gizmos.DrawCube(Vector3.zero, Size);

            Gizmos.color = oldColor;
            Gizmos.matrix = oldMatrix;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Size = Collider.size;
        }
    }
}
