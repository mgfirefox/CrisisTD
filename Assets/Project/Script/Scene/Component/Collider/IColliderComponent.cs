using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface IColliderComponent : IComponent
    {
        int Layer { get; set; }

        Vector3 GetClosestPosition(Vector3 position);
        bool IsPositionWithin(Vector3 position, float epsilon);
    }
}
