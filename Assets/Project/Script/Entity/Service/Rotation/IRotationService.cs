using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface IRotationService : IService
    {
        float Rotate(float orientation, float direction, float speed);

        float Rotate(float orientation, float direction, float speed, float minSlope,
            float maxSlope);

        Quaternion RotateTo(Vector3 position, Vector3 targetPosition);
    }
}
