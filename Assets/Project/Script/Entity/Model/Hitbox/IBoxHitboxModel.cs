using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface IBoxHitboxModel : IHitboxModel
    {
        Vector3 Size { get; }
        float Length { get; }
        float Height { get; }
        float Width { get; }
    }
}
