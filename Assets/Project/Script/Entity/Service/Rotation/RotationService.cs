using UnityEngine;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class RotationService : AbstractService, IRotationService
    {
        private readonly ITimeService timeService;

        [Inject]
        public RotationService(ITimeService timeService)
        {
            this.timeService = timeService;
        }

        public float Rotate(float orientation, float direction, float speed)
        {
            float deltaTime = timeService.DeltaTime;

            direction = direction.Normalize();

            float rotation = direction * speed * deltaTime;

            return orientation + rotation;
        }

        public float Rotate(float orientation, float direction, float speed, float minSlope,
            float maxSlope)
        {
            float deltaTime = timeService.DeltaTime;

            direction = direction.Normalize();

            float rotation = direction * speed * deltaTime;

            return Mathf.Clamp(orientation + rotation, minSlope, maxSlope);
        }

        public Quaternion RotateTo(Vector3 position, Vector3 targetPosition)
        {
            Vector3 forward = Vector3Utility.GetTranslation(position, targetPosition);

            return Quaternion.LookRotation(forward);
        }
    }
}
