using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class ArcAngleAttackActionView : AbstractAttackActionView, IArcAngleAttackActionView
    {
        [SerializeField]
        [BoxGroup("ArcAngleAttack")]
        [ReadOnly]
        private float arcAngle;

        [SerializeField]
        [BoxGroup("ArcAngleAttack")]
        [ReadOnly]
        private int maxPelletCount;
        [SerializeField]
        [BoxGroup("ArcAngleAttack")]
        [ReadOnly]
        private int maxPelletHitCount;
        public float ArcAngle { get => arcAngle; set => arcAngle = value; }

        public int MaxPelletCount { get => maxPelletCount; set => maxPelletCount = value; }
        public int MaxPelletHitCount { get => maxPelletHitCount; set => maxPelletHitCount = value; }
    }
}
