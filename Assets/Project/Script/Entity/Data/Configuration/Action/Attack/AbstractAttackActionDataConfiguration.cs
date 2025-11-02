using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractAttackActionDataConfiguration : ScriptableObject
    {
        [SerializeField]
        [BoxGroup("Attack")]
        [MinValue(0.0f)]
        [MaxValue(float.MaxValue)]
        private float damage;
        [SerializeField]
        [BoxGroup("Attack")]
        [MinValue(0.0f)]
        [MaxValue(float.MaxValue)]
        private float fireRate;

        public abstract AttackActionType Type { get; }

        public float Damage => damage;
        public float FireRate => fireRate;
    }
}
