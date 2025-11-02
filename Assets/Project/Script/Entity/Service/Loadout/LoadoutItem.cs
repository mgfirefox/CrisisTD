using System;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    [Serializable]
    public class LoadoutItem
    {
        [SerializeField]
        private TowerId towerId = TowerId.Undefined;

        public TowerId TowerId { get => towerId; set => towerId = value; }
    }
}
