using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class SupportTowerAction :
        AbstractTowerAction<SupportTowerActionData, ISupportTowerActionView, ITowerView,
            ITowerTargetService>, ISupportTowerAction
    {
        private readonly IBuffActionFactory actionFactory;

        private readonly IList<IBuffAction> actions = new List<IBuffAction>();

        [Inject]
        public SupportTowerAction(ISupportTowerActionView view, ITowerTargetService targetService,
            IRangeView rangeView, IBuffActionFactory actionFactory, Scene scene) : base(view,
            targetService, rangeView, scene)
        {
            this.actionFactory = actionFactory;
        }

        public override void Perform()
        {
            base.Perform();

            foreach (IBuffAction action in actions)
            {
                action.Perform();
            }
        }

        protected override void OnInitialized(SupportTowerActionData data)
        {
            base.OnInitialized(data);

            InitializeActions(data.ActionDataList);
        }

        private void InitializeActions(IList<AbstractBuffActionData> actionDataList)
        {
            foreach (AbstractBuffActionData actionData in actionDataList)
            {
                if (actionFactory.TryCreate(actionData, View, out IBuffAction action))
                {
                    actions.Add(action);
                }
                else
                {
                    // TODO: Change Warning
                    Debug.LogWarning(
                        $"Failed to create Object of type {typeof(IBuffAction)} with ID \"${actionData.Type}\"");
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
            foreach (IBuffAction action in actions)
            {
                action.Destroy();
            }
            actions.Clear();
        }
    }
}
