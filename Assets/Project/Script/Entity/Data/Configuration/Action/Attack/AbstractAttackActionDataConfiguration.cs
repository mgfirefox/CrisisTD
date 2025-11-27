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
        [MaxValue(100.0f)]
        private float armorPiercing; // %
        [SerializeField]
        [BoxGroup("Attack")]
        [MinValue(0.0f)]
        [MaxValue(float.MaxValue)]
        private float fireRate;

        public abstract AttackActionType Type { get; }

        public float Damage => damage;
        public float ArmorPiercing => armorPiercing / 100.0f; // 1/100
        public float FireRate => fireRate;
    }
}
