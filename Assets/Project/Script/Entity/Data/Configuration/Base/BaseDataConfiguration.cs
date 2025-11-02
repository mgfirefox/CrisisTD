using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    [CreateAssetMenu(fileName = "BaseDataConfiguration", menuName = "DataConfiguration/Base")]
    public class BaseDataConfiguration : ScriptableObject
    {
        [SerializeField]
        [BoxGroup("Health")]
        [MinValue(0.0f)]
        [MaxValue(float.MaxValue)]
        private float health;

        public float Health => health;
    }
}
