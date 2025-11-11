using System;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    [Serializable]
    public class BranchLevel : ICloneable
    {
        [SerializeField]
        private BranchType type;
        [SerializeField]
        private int index;

        public BranchType Type => type;
        public int Index { get => index; set => index = value; }

        public BranchLevel() : this(BranchType.Zero)
        {
        }

        public BranchLevel(BranchType type)
        {
            this.type = type;
        }

        protected bool Equals(BranchLevel other)
        {
            return type == other.type && index == other.index;
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
            return Equals((BranchLevel)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int)type, index);
        }

        public object Clone()
        {
            var branchLevelIndex = new BranchLevel(type)
            {
                index = index,
            };

            return branchLevelIndex;
        }
    }
}
