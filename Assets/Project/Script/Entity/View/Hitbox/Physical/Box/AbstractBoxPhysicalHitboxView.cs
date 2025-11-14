using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractBoxPhysicalHitboxView : AbstractPhysicalHitboxView<IBoxCollider, BoxCollider>,
        IBoxPhysicalHitboxView
    {
        public Vector3 Size { get => Collider.Size; set => Collider.Size = value; }
        public float Length { get => Collider.Length; set => Collider.Length = value; }
        public float Height { get => Collider.Height; set => Collider.Height = value; }
        public float Width { get => Collider.Width; set => Collider.Width = value; }

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
    }
}
