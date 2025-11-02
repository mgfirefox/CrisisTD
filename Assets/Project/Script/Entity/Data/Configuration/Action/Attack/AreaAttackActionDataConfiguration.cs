using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    [CreateAssetMenu(fileName = "AttackActionDataConfiguration",
        menuName = "DataConfiguration/Action/Attack/Area")]
    public class AreaAttackActionDataConfiguration : AbstractAttackActionDataConfiguration
    {
        [SerializeField]
        [BoxGroup("AreaAttack")]
        [MinValue(0.0f)]
        [MaxValue(float.MaxValue)]
        private float innerRadius;
        [SerializeField]
        [BoxGroup("AreaAttack")]
        [MinValue(0.0f)]
        [MaxValue(float.MaxValue)]
        private float outerRadius;

        [SerializeField]
        [BoxGroup("AreaAttack")]
        [MinValue(0)]
        [MaxValue(Constant.maxHitboxTargetCount)]
        private int maxHitCount;

        public override AttackActionType Type => AttackActionType.Area;

        public float InnerRadius => innerRadius;
        public float OuterRadius
        {
            get
            {
                if (outerRadius < innerRadius)
                {
                    Debug.LogWarning("Outer Radius cannot be less than Inner Radius", this);

                    outerRadius = innerRadius;
                }

                return outerRadius;
            }
        }

        public int MaxHitCount => maxHitCount;
    }
}
