using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface IBoxPhysicalHitboxView : IPhysicalHitboxView, IBoxHitboxModel
    {
        new Vector3 Size { get; set; }
        new float Length { get; set; }
        new float Height { get; set; }
        new float Width { get; set; }
    }

    public interface IBoxPhysicalHitboxView<out TITargetView> : IBoxPhysicalHitboxView,
        IPhysicalHitboxView<TITargetView>
        where TITargetView : class, IView
    {
    }
}
