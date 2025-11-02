using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    [CreateAssetMenu(fileName = "EnemyDataConfiguration", menuName = "DataConfiguration/Enemy")]
    public class EnemyDataConfiguration : ScriptableObject
    {
        [SerializeField]
        [BoxGroup("Transform")]
        [MinValue(0.0f)]
        [MaxValue(float.MaxValue)]
        private float movementSpeed;

        [SerializeField]
        [BoxGroup("Health")]
        [MinValue(0.0f)]
        [MaxValue(float.MaxValue)]
        private float health;

        public float MovementSpeed => movementSpeed;

        public float Health => health;
    }
}
