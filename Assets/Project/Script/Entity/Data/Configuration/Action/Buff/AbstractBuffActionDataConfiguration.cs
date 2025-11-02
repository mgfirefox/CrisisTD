using System.Linq;
using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractBuffActionDataConfiguration : ScriptableObject
    {
        [SerializeField]
        [BoxGroup("Buff")]
        [Dropdown("validEffectTypes")]
        private EffectType type;
        private EffectType[] validEffectTypes = EffectTypeValidator.ValidTypes.ToArray();
        [SerializeField]
        [BoxGroup("Buff")]
        [MinValue(Constant.minBuffEffectValue * 100)]
        [MaxValue(Constant.maxBuffEffectValue * 100)]
        private float value; // %

        public abstract BuffActionType Type { get; }

        public EffectType BuffType => type;
        public float Value => value / 100; // 1/100
    }
}
