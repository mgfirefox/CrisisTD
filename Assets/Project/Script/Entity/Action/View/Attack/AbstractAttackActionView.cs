using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractAttackActionView : AbstractActionView, IAttackActionView
    {
        [SerializeField]
        [BoxGroup("Attack")]
        [ReadOnly]
        private float damage;
        [SerializeField]
        [BoxGroup("Attack")]
        [ReadOnly]
        private float armorPiercing;

        [SerializeField]
        [BoxGroup("Cooldown")]
        [ReadOnly]
        private float maxCooldown;
        [SerializeField]
        [BoxGroup("Cooldown")]
        [ReadOnly]
        private float cooldown;

        public float Damage { get => damage; set => damage = value; }
        public float ArmorPiercing  { get => armorPiercing; set => armorPiercing = value; }

        public float MaxCooldown { get => maxCooldown; set => maxCooldown = value; }
        public float Cooldown { get => cooldown; set => cooldown = value; }
    }
}
