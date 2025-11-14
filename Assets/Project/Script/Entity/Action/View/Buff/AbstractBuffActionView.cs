using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractBuffActionView : AbstractActionView, IBuffActionView
    {
        [SerializeField]
        [BoxGroup("Buff")]
        [ReadOnly]
        private EffectType type;

        [SerializeField]
        [BoxGroup("Buff")]
        [ReadOnly]
        private float maxValue;
        [SerializeField]
        [BoxGroup("Buff")]
        [ReadOnly]
        private float value;

        public EffectType Type { get => type; set => type = value; }

        public float MaxValue { get => maxValue; set => maxValue = value; }
        public float Value { get => value; set => this.value = value; }
    }
}
