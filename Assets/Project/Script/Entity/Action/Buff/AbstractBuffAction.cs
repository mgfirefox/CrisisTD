using System.Collections.Generic;

namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractBuffAction<TData, TIView> : AbstractAction<TData, TIView>,
        IBuffAction<TData>
        where TData : AbstractBuffActionData
        where TIView : class, IBuffActionView
    {
        protected IAttackTowerTargetService TargetService { get; }

        public EffectType Type { get; private set; }
        public float MaxValue { get; private set; }
        public float Value { get; protected set; }

        protected AbstractBuffAction(TIView view, IAttackTowerTargetService targetService,
            Scene scene) : base(view, scene)
        {
            TargetService = targetService;
        }

        protected abstract void ApplyBuff(IReadOnlyList<ITowerView> targets);

        protected override void OnInitialized(TData data)
        {
            base.OnInitialized(data);

            Type = data.BuffType;
            MaxValue = data.MaxValue;
            Value = data.Value;

            View.Type = Type;
            View.MaxValue = MaxValue;
            View.Value = Value;
        }
    }
}
