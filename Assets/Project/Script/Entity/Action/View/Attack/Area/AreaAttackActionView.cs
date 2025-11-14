using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class AreaAttackActionView : AbstractAttackActionView, IAreaAttackActionView
    {
        [SerializeField]
        [BoxGroup("AreaAttack")]
        [ReadOnly]
        private float innerRadius;
        [SerializeField]
        [BoxGroup("AreaAttack")]
        [ReadOnly]
        private float outerRadius;

        [SerializeField]
        [BoxGroup("AreaAttack")]
        [ReadOnly]
        private int maxHitCount;

        public float InnerRadius { get => innerRadius; set => innerRadius = value; }
        public float OuterRadius { get => outerRadius; set => outerRadius = value; }

        public int MaxHitCount { get => maxHitCount; set => maxHitCount = value; }
    }
}
