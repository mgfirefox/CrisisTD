using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface ITranslationService : IService
    {
        Vector3 Translate(Vector3 position, Quaternion orientation, Vector3 direction, float speed);

        TranslationToResult TranslateTo(Vector3 position, Vector3 targetPosition, float speed,
            float epsilon, float travelledDistance = 0.0f);
    }
}
