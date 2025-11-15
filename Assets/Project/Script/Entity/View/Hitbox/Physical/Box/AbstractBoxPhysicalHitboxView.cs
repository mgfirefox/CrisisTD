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
    }
}
