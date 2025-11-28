using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class TowerInteractionAction :
        AbstractUiAction<TowerInteractionActionData, ITowerInteractionActionView,
            ITowerInteractionActionUi>, ITowerInteractionAction
    {
        private readonly IEconomyService economyService;
        
        private readonly ICameraView camera;

        private readonly ITowerTargetRayView rayView;

        private ITowerView interactingTower;

        public bool IsInteracting => interactingTower != null;

        [Inject]
        public TowerInteractionAction(ITowerInteractionActionView view, IEconomyService economyService,
            ITowerInteractionActionUi ui, ITowerTargetRayView rayView, ICameraService cameraService,
            Scene scene) : base(view, ui, scene)
        {
            this.economyService = economyService;
            this.rayView = rayView;
            camera = cameraService.MainCamera;
        }

        public override void OnSceneStarted()
        {
            base.OnSceneStarted();

            Ui.SingleBranchUpgradeButtonClicked += UpgradeBranch;

            Ui.FirstBranchUpgradeButtonClicked += UpgradeFirstBranch;
            Ui.SecondBranchUpgradeButtonClicked += UpgradeSecondBranch;

            Ui.SellButtonClicked += Sell;
        }

        public override void OnSceneFinished()
        {
            base.OnSceneFinished();

            Ui.SingleBranchUpgradeButtonClicked -= UpgradeBranch;

            Ui.FirstBranchUpgradeButtonClicked -= UpgradeFirstBranch;
            Ui.SecondBranchUpgradeButtonClicked -= UpgradeSecondBranch;
            
            Ui.SellButtonClicked -= Sell;
        }

        public override void Perform()
        {
            Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();

            Ray cursorRay = camera.GetRayFromScreenPosition(mouseScreenPosition);

            rayView.SetNormalizedRay(cursorRay.origin, cursorRay.direction);
            if (rayView.TryCastTarget(out RayHit<ITowerView> hit))
            {
                if (interactingTower == hit.Target)
                {
                    return;
                }

                Cancel();

                interactingTower = hit.Target;
                interactingTower.ShowInteraction();

                View.IsInteracting = IsInteracting;

                Ui.InteractingTower = interactingTower;
                Ui.Show();
            }
        }

        public void UpgradeFirstBranch()
        {
            if (!IsInteracting)
            {
                return;
            }
            
            float epsilon = Scene.Settings.MathSettings.Epsilon;

            IList<NextBranchLevel> nextLevels = interactingTower.NextLevels;
            if (nextLevels.Count == 0)
            {
                return;
            }
            
            NextBranchLevel nextLevel = nextLevels[0];
            if (nextLevel.Type is not (BranchType.Zero or BranchType.First))
            {
                return;
            }

            if (!economyService.TryUpgradeTower(interactingTower, nextLevel, epsilon))
            {
                return;
            }

            interactingTower.UpgradeFirstBranch();

            Ui.InteractingTower = interactingTower;
        }

        public void UpgradeSecondBranch()
        {
            if (!IsInteracting)
            {
                return;
            }
            
            float epsilon = Scene.Settings.MathSettings.Epsilon;

            IList<NextBranchLevel> nextLevels = interactingTower.NextLevels;
            
            NextBranchLevel nextLevel;
            switch (nextLevels.Count)
            {
                case 0:
                    return;
                case 1:
                    nextLevel = nextLevels[0];
                break;
                default:
                    nextLevel = nextLevels[1];
                break;
            }
            if (nextLevel.Type is not (BranchType.Zero or BranchType.Second))
            {
                return;
            }

            if (!economyService.TryUpgradeTower(interactingTower, nextLevel, epsilon))
            {
                return;
            }

            interactingTower.UpgradeSecondBranch();

            Ui.InteractingTower = interactingTower;
        }

        public void Sell()
        {
            if (!IsInteracting)
            {
                return;
            }
            
            economyService.SellTower(interactingTower);

            interactingTower.Destroy();
            interactingTower = null;

            View.IsInteracting = IsInteracting;

            Ui.Hide();
        }

        public void Cancel()
        {
            if (!IsInteracting)
            {
                return;
            }

            interactingTower.HideInteraction();
            interactingTower = null;

            View.IsInteracting = IsInteracting;

            Ui.Hide();
        }

        private void UpgradeBranch()
        {
            if (!BranchLevelValidator.TryValidate(interactingTower.Level))
            {
                throw new InvalidArgumentException();
            }
            
            float epsilon = Scene.Settings.MathSettings.Epsilon;

            switch (interactingTower.Level.Type)
            {
                case BranchType.Zero:
                    if (interactingTower.Level.Index == interactingTower.MaxZeroBranchIndex)
                    {
                        return;
                    }

                    UpgradeFirstBranch();
                break;
                case BranchType.First:
                    UpgradeFirstBranch();
                break;
                case BranchType.Second:
                    UpgradeSecondBranch();
                break;
                case BranchType.Undefined:
                default:
                    throw new InvalidArgumentException();
            }
        }

        protected override void OnInitialized(TowerInteractionActionData data)
        {
            base.OnInitialized(data);

            View.IsInteracting = IsInteracting;

            Ui.Hide();

            rayView.Initialize();

            rayView.MaxDistance = Constant.towerInteractionMaxDistance;
            rayView.CollisionLayerMask = Constant.TowerSelectionLayerMask;
        }

        protected override void OnDestroying()
        {
            rayView.Destroy();

            base.OnDestroying();
        }
    }
}
