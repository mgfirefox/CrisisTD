using UnityEngine;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class TranslationService : AbstractService, ITranslationService
    {
        private readonly ITimeService timeService;

        [Inject]
        public TranslationService(ITimeService timeService)
        {
            this.timeService = timeService;
        }

        public Vector3 Translate(Vector3 position, Quaternion orientation, Vector3 direction,
            float speed)
        {
            float deltaTime = timeService.DeltaTime;

            direction.Normalize();

            Vector3 translation = direction * speed * deltaTime;

            orientation = Quaternion.Euler(0.0f, orientation.eulerAngles.y, 0.0f);

            Vector3 orientatedTranslation = orientation * translation;

            return position + orientatedTranslation;
        }

        public TranslationToResult TranslateTo(Vector3 position, Vector3 targetPosition,
            float speed, float epsilon, float travelledDistance = 0.0f)
        {
            float deltaTime = timeService.DeltaTime;

            float translationDistance = speed * deltaTime - travelledDistance;
            if (translationDistance.IsApproximateNonPositive(epsilon))
            {
                return new TranslationToResult
                {
                    position = position,
                    travelledDistance = translationDistance,
                };
            }

            Vector3 targetDirection = Vector3Utility.GetTranslation(position, targetPosition);
            float targetDistance = targetDirection.magnitude;
            targetDirection.Normalize();

            if (translationDistance.IsLessThanApproximately(targetDistance, epsilon))
            {
                Vector3 translation = targetDirection * translationDistance;

                return new TranslationToResult
                {
                    position = position + translation,
                    travelledDistance = travelledDistance + translationDistance,
                };
            }

            return new TranslationToResult
            {
                position = targetPosition,
                travelledDistance = travelledDistance + targetDistance,
            };
        }
    }
}
