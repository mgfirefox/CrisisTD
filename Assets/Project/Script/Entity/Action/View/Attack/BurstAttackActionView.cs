using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class BurstAttackActionView : AbstractAttackActionView, IBurstAttackActionView
    {
        [SerializeField]
        [BoxGroup("BurstAttack")]
        [ReadOnly]
        private int maxBurstShotCount;
        [SerializeField]
        [BoxGroup("BurstAttack")]
        [ReadOnly]
        private int burstShotCount;

        [SerializeField]
        [BoxGroup("BurstCooldown")]
        [ReadOnly]
        private float burstMaxCooldown;
        [SerializeField]
        [BoxGroup("BurstCooldown")]
        [ReadOnly]
        private float burstCooldown;


        public int MaxBurstShotCount { get => maxBurstShotCount; set => maxBurstShotCount = value; }
        public int BurstShotCount { get => burstShotCount; set => burstShotCount = value; }

        public float BurstMaxCooldown { get => burstMaxCooldown; set => burstMaxCooldown = value; }
        public float BurstCooldown { get => burstCooldown; set => burstCooldown = value; }
    }
}
