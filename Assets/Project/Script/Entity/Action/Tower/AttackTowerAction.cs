using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class AttackTowerAction :
        AbstractTowerAction<AttackTowerActionData, IAttackTowerActionView, IEnemyView,
            IEnemyTargetService>, IAttackTowerAction
    {
        private readonly IAttackActionFactory actionFactory;

        private readonly IList<IAttackAction> actions = new List<IAttackAction>();

        [Inject]
        public AttackTowerAction(IAttackTowerActionView view, IEnemyTargetService targetService,
            IRangeView rangeView, IAttackActionFactory actionFactory, Scene scene) : base(view,
            targetService, rangeView, scene)
        {
            this.actionFactory = actionFactory;
        }

        public override void Perform()
        {
            base.Perform();

            foreach (IAttackAction action in actions)
            {
                action.Perform();
            }
        }

        protected override void OnInitialized(AttackTowerActionData data)
        {
            base.OnInitialized(data);

            InitializeActions(data.ActionDataList);
        }

        private void InitializeActions(IList<AbstractAttackActionData> actionDataList)
        {
            foreach (AbstractAttackActionData actionData in actionDataList)
            {
                if (actionFactory.TryCreate(actionData, View, out IAttackAction action))
                {
                    actions.Add(action);
                }
                else
                {
                    // TODO: Change Warning
                    Debug.LogWarning(
                        $"Failed to create Object of type {typeof(IAttackAction)} with ID \"${actionData.Type}\"");
                }
            }
        }

        protected override void OnDestroying()
        {
            DestroyActions();

            base.OnDestroying();
        }

        private void DestroyActions()
        {
            foreach (IAttackAction action in actions)
            {
                action.Destroy();
            }
            actions.Clear();
        }
    }
}
