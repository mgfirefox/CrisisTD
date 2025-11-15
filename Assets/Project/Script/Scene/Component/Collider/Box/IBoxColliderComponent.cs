using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface IBoxColliderComponent : IColliderComponent
    {
        Vector3 Size { get; set; }
        float Length { get; set; }
        float Height { get; set; }
        float Width { get; set; }

        void SetSize(float length, float height, float width);
    }
}
