using System;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    [Serializable]
    public class LevelIndex : ICloneable
    {
        [SerializeField]
        private int branch0;
        [SerializeField]
        private int branch1;
        [SerializeField]
        private int branch2;

        public int Branch0 { get => branch0; set => branch0 = value; }
        public int Branch1 { get => branch1; set => branch1 = value; }
        public int Branch2 { get => branch2; set => branch2 = value; }

        protected bool Equals(LevelIndex other)
        {
            return branch0 == other.branch0 && branch1 == other.branch1 && branch2 == other.branch2;
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
            return Equals((LevelIndex)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(branch0, branch1, branch2);
        }

        public object Clone()
        {
            var levelIndex = new LevelIndex
            {
                Branch0 = branch0,
                Branch1 = branch1,
                Branch2 = branch2,
            };

            return levelIndex;
        }
    }
}
