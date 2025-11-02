using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    [CreateAssetMenu(fileName = "AttackActionDataConfiguration",
        menuName = "DataConfiguration/Action/Attack/ArcAngle")]
    public class ArcAngleAttackActionDataConfiguration : AbstractAttackActionDataConfiguration
    {
        [SerializeField]
        [BoxGroup("ArcAngleAttack")]
        [MinValue(0.0f)]
        [MaxValue(179.0f)]
        private float arcAngle; // °

        [SerializeField]
        [BoxGroup("ArcAngleAttack")]
        [MinValue(0)]
        [MaxValue(int.MaxValue)]
        private int pelletCount;
        [SerializeField]
        [BoxGroup("ArcAngleAttack")]
        [MinValue(0)]
        [MaxValue(int.MaxValue)]
        private int maxPelletHitCount;

        public override AttackActionType Type => AttackActionType.ArcAngle;

        public float ArcAngle => arcAngle * Mathf.Deg2Rad; // rad

        public int PelletCount => pelletCount;
        public int MaxPelletHitCount => maxPelletHitCount;
    }
}
