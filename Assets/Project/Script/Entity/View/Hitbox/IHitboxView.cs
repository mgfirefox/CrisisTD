using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface IHitboxView : ICollisionView, IHitboxModel
    {
        new Vector3 Position { get; set; }
        new Quaternion Orientation { get; set; }
    }
}
