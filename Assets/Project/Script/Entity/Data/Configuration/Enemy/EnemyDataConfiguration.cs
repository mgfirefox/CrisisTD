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

        [SerializeField]
        [BoxGroup("ArmoredHealth")]
        [MinValue(0.0f)]
        [MaxValue(float.MaxValue)]
        private float shield;
        [SerializeField]
        [BoxGroup("ArmoredHealth")]
        [MinValue(0.0f)]
        [MaxValue(100.0f)]
        private float armor; // %

        public float MovementSpeed => movementSpeed;

        public float Health => health;
        
        public float Shield => shield;
        public float Armor => armor / 100.0f; // 1/100
    }
}
