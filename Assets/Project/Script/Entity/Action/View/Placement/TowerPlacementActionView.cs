using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class TowerPlacementActionView : AbstractActionView, ITowerPlacementActionView
    {
        [SerializeField]
        [BoxGroup("Placement")]
        [ReadOnly]
        private int selectedIndex;

        [SerializeField]
        [BoxGroup("Placement")]
        [ReadOnly]
        private int limit;
        [SerializeField]
        [BoxGroup("Placement")]
        [ReadOnly]
        private int count;

        [SerializeField]
        [BoxGroup("Placement")]
        [ReadOnly]
        private bool isPlacing;
        [SerializeField]
        [BoxGroup("Placement")]
        [ReadOnly]
        private bool isPlacementSuitable;

        [SerializeField]
        [BoxGroup("Placement")]
        [ReadOnly]
        private bool isLimitReached;

        public int SelectedIndex { get => selectedIndex; set => selectedIndex = value; }

        public int Limit { get => limit; set => limit = value; }
        public int Count { get => count; set => count = value; }

        public bool IsPlacing { get => isPlacing; set => isPlacing = value; }
        public bool IsPlacementSuitable
        {
            get => isPlacementSuitable;
            set => isPlacementSuitable = value;
        }

        public bool IsLimitReached { get => isLimitReached; set => isLimitReached = value; }
    }
}
