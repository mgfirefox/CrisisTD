using System;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    [Serializable]
    public class NextBranchLevel : BranchLevel
    {
        [SerializeField]
        private float upgradeCost;

        public float UpgradeCost { get => upgradeCost; set => upgradeCost = value; }

        public NextBranchLevel()
        {
        }

        public NextBranchLevel(BranchType type) : base(type)
        {
        }

        protected bool Equals(NextBranchLevel other)
        {
            return base.Equals(other) && upgradeCost.Equals(other.upgradeCost);
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != GetType())
            {
                return false;
            }
            return Equals((NextBranchLevel)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), upgradeCost);
        }

        public new object Clone()
        {
            var branchLevelIndex = new NextBranchLevel(Type)
            {
                Index = Index,
                UpgradeCost = UpgradeCost,
            };

            return branchLevelIndex;
        }
    }
}
