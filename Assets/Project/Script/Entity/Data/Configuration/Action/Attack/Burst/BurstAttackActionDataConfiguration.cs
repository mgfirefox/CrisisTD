using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    [CreateAssetMenu(fileName = "AttackActionDataConfiguration",
        menuName = "DataConfiguration/Action/Attack/Burst")]
    public class BurstAttackActionDataConfiguration : AbstractAttackActionDataConfiguration
    {
        [SerializeField]
        [BoxGroup("BurstAttack")]
        [MinValue(0)]
        [MaxValue(int.MaxValue)]
        private int burstShotCount;
        [SerializeField]
        [BoxGroup("BurstAttack")]
        [MinValue(0)]
        [MaxValue(int.MaxValue)]
        private float burstCooldown;

        public override AttackActionType Type => AttackActionType.Burst;

        public int BurstShotCount => burstShotCount;
        public float BurstCooldown => burstCooldown;
    }
}
