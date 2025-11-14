using System;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    [Serializable]
    public class LoadoutItem : ICloneable
    {
        [SerializeField]
        private TowerId towerId = TowerId.Undefined;

        public TowerId TowerId { get => towerId; set => towerId = value; }

        protected bool Equals(LoadoutItem other)
        {
            return towerId == other.towerId;
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
            return Equals((LoadoutItem)obj);
        }

        public override int GetHashCode()
        {
            return (int)towerId;
        }

        public object Clone()
        {
            var loadoutItem = new LoadoutItem
            {
                towerId = towerId,
            };

            return loadoutItem;
        }
    }
}
