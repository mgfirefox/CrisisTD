using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public struct TranslationToResult
    {
        public static TranslationToResult Zero { get; } = new()
        {
            position = Vector3.zero,
        };

        public Vector3 position;
        public float travelledDistance;

        public TranslationToResult(Vector3 position) : this()
        {
            this.position = position;
        }

        public TranslationToResult(Vector3 position, float travelledDistance) : this(position)
        {
            this.travelledDistance = travelledDistance;
        }
    }
}
