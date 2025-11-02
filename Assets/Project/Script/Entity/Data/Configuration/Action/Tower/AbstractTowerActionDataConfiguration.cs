using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractTowerActionDataConfiguration : ScriptableObject
    {
        [SerializeField]
        [BoxGroup("Target")]
        [MinValue(0.0f)]
        [MaxValue(float.MaxValue)]
        protected float range;

        public float Range => range;
    }
}
