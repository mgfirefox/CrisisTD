using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class TowerPresenter : AbstractPresenter<TowerData, ITowerView>, ITowerPresenter,
        ISceneTickedListener
    {
        private readonly ITowerActionFactory actionFactory;

        private readonly ITowerTransformService transformService;

        private readonly ITowerAllEffectService effectService;

        private readonly ILevelService levelService;

        private TowerId id;

        private TowerType type;
        private readonly IList<ITowerAction> actions = new List<ITowerAction>();

        private TargetPriority priority;

        [Inject]
        public TowerPresenter(ITowerView view, ITowerTransformService transformService,
            ITowerAllEffectService effectService, ILevelService levelService,
            ITowerActionFactory actionFactory, Scene scene) : base(view, scene)
        {
            this.transformService = transformService;
            this.effectService = effectService;
            this.levelService = levelService;
            this.actionFactory = actionFactory;
        }

        public override void OnSceneStarted()
        {
            base.OnSceneStarted();

            levelService.Changed += OnLevelChanged;

            effectService.RangeChanged += OnEffectRangeChanged;

            View.EffectApplied += OnEffectApplied;
            View.Branch1Upgraded += OnBranch1Upgraded;
            View.Branch2Upgraded += OnBranch2Upgraded;
            View.InteractionShown += OnInteractionShown;
            View.InteractionHidden += OnInteractionHidden;
        }

        public override void OnSceneFinished()
        {
            base.OnSceneFinished();

            levelService.Changed -= OnLevelChanged;

            effectService.RangeChanged -= OnEffectRangeChanged;

            View.EffectApplied -= OnEffectApplied;
            View.Branch1Upgraded -= OnBranch1Upgraded;
            View.Branch2Upgraded -= OnBranch2Upgraded;
            View.InteractionShown -= OnInteractionShown;
            View.InteractionHidden -= OnInteractionHidden;
        }

        public void OnSceneTicked()
        {
            foreach (ITowerAction action in actions)
            {
                action.Perform();
            }
        }

        protected override void OnInitialized(TowerData data)
        {
            base.OnInitialized(data);

            id = data.Id;

            View.Id = id;

            type = data.Type;

            View.Type = type;

            priority = data.Priority;

            View.Priority = priority;

            transformService.Initialize(data.TransformServiceData);

            View.Position = transformService.Position;
            View.Orientation = transformService.Orientation;

            effectService.Initialize(data.AllEffectServiceData);

            View.RangeEffect = effectService.RangeEffect;

            levelService.Initialize(data.LevelServiceData);

            View.MaxBranch0Index = levelService.MaxBranch0Index;
            View.MaxBranch1Index = levelService.MaxBranch1Index;
            View.MaxBranch2Index = levelService.MaxBranch2Index;

            HideInteraction();
        }

        private void InitializeActions(IList<AbstractTowerActionData> actionDataList)
        {
            foreach (AbstractTowerActionData actionData in actionDataList)
            {
                if (actionFactory.TryCreate(type, actionData, View, out ITowerAction action))
                {
                    actions.Add(action);
                }
                else
                {
                    // TODO: Change Warning
                    Debug.LogWarning(
                        $"Failed to create Object of type {typeof(ITowerAction)} with ID \"${type}\"");
                }
            }
        }

        protected override void OnDestroying()
        {
            DestroyActions();

            levelService.Destroy();

            effectService.Destroy();

            transformService.Destroy();

            base.OnDestroying();
        }

        private void DestroyActions()
        {
            foreach (ITowerAction action in actions)
            {
                action.Destroy();
            }
            actions.Clear();
        }

        private void OnEffectRangeChanged(Effect effect)
        {
            View.RangeEffect = effect.Clone() as Effect;
        }

        private void OnLevelChanged(LevelItem item)
        {
            DestroyActions();
            InitializeActions(item.ActionDataList);

            View.Index = levelService.Index;

            effectService.Reapply();
        }

        private void OnEffectApplied(Effect effect, ITowerView source)
        {
            effectService.Apply(effect.Clone() as Effect, source);
        }

        private void OnBranch1Upgraded()
        {
            levelService.UpgradeBranch1();
        }

        private void OnBranch2Upgraded()
        {
            levelService.UpgradeBranch2();
        }

        private void OnInteractionShown()
        {
            ShowInteraction();
        }

        private void ShowInteraction()
        {
            foreach (ITowerAction action in actions)
            {
                action.ShowInteraction();
            }
        }

        private void OnInteractionHidden()
        {
            HideInteraction();
        }

        private void HideInteraction()
        {
            foreach (ITowerAction action in actions)
            {
                action.HideInteraction();
            }
        }
    }
}
