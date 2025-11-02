using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class AbstractTowerActionView : AbstractActionView, ITowerActionView
    {
        [SerializeField]
        [BoxGroup("Target")]
        [ReadOnly]
        private float rangeRadius;

        [SerializeField]
        [BoxGroup("Target")]
        [ReadOnly]
        private TargetPriority targetPriority;

        public float RangeRadius { get => rangeRadius; set => rangeRadius = value; }

        public TargetPriority TargetPriority
        {
            get => targetPriority;
            set => targetPriority = value;
        }
    }
}
