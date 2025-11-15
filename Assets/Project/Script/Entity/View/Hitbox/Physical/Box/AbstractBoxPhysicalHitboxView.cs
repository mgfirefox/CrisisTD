using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public abstract class
        AbstractBoxPhysicalHitboxView : AbstractPhysicalHitboxView<IBoxColliderComponent, BoxColliderComponent>,
        IBoxPhysicalHitboxView
    {
        public Vector3 Size { get => Collider.Size; set => Collider.Size = value; }
        public float Length { get => Collider.Length; set => Collider.Length = value; }
        public float Height { get => Collider.Height; set => Collider.Height = value; }
        public float Width { get => Collider.Width; set => Collider.Width = value; }
    }

    public abstract class AbstractBoxPhysicalHitboxView<TITargetView> :
        AbstractPhysicalHitboxView<IBoxColliderComponent, BoxColliderComponent, TITargetView>,
        IBoxPhysicalHitboxView<TITargetView>
        where TITargetView : class, IView
    {
        public Vector3 Size { get => Collider.Size; set => Collider.Size = value; }
        public float Length { get => Collider.Length; set => Collider.Length = value; }
        public float Height { get => Collider.Height; set => Collider.Height = value; }
        public float Width { get => Collider.Width; set => Collider.Width = value; }
    }
}
