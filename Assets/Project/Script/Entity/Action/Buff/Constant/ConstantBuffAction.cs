using System;
using System.Collections.Generic;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class ConstantBuffAction :
        AbstractBuffAction<ConstantBuffActionData, IConstantBuffActionView>, IConstantBuffAction
    {
        private readonly ITowerView towerView;

        private readonly ISet<ITowerView> buffedTargets = new HashSet<ITowerView>();
        private readonly IDictionary<ITowerView, Action> buffedTargetDestroyingActions =
            new Dictionary<ITowerView, Action>();

        [Inject]
        public ConstantBuffAction(IConstantBuffActionView view,
            IAttackTowerTargetService targetService, ITowerView towerView, Scene scene) : base(view,
            targetService, scene)
        {
            this.towerView = towerView;
        }

        public override void Perform()
        {
            IReadOnlyList<ITowerView> targets = TargetService.Targets;

            IList<ITowerView> newTargets = new List<ITowerView>();

            foreach (ITowerView target in targets)
            {
                if (buffedTargets.Contains(target))
                {
                    continue;
                }

                newTargets.Add(target);
                AddBuffedTarget(target);
            }

            ApplyBuff(newTargets.AsReadOnly());
        }

        protected override void ApplyBuff(IReadOnlyList<ITowerView> targets)
        {
            foreach (ITowerView target in targets)
            {
                var effect = new BuffEffect
                {
                    Type = Type,
                    Value = Value,
                };

                target.ApplyEffect(effect.Clone() as BuffEffect, towerView);
            }
        }

        private void AddBuffedTarget(ITowerView buffedTarget)
        {
            Action buffedTargetDestroyingAction = () => RemoveBuffedTarget(buffedTarget);

            buffedTarget.Destroying += buffedTargetDestroyingAction;

            buffedTargets.Add(buffedTarget);
            buffedTargetDestroyingActions[buffedTarget] = buffedTargetDestroyingAction;
        }

        private void RemoveBuffedTarget(ITowerView buffedTarget)
        {
            buffedTarget.Destroying -= buffedTargetDestroyingActions[buffedTarget];

            buffedTargets.Remove(buffedTarget);
            buffedTargetDestroyingActions.Remove(buffedTarget);
        }

        protected override void OnInitialized(ConstantBuffActionData data)
        {
            base.OnInitialized(data);

            Value = MaxValue;

            View.Value = Value;
        }

        protected override void OnDestroying()
        {
            ClearBuffedTargets();

            base.OnDestroying();
        }

        private void ClearBuffedTargets()
        {
            foreach (ITowerView buffedTarget in buffedTargets)
            {
                buffedTarget.Destroying -= buffedTargetDestroyingActions[buffedTarget];
            }
            buffedTargets.Clear();
            buffedTargetDestroyingActions.Clear();
        }
    }
}
