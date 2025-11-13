using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class TowerInteractionAction :
        AbstractUiAction<TowerInteractionActionData, ITowerInteractionActionView,
            ITowerInteractionActionUi>, ITowerInteractionAction
    {
        private readonly ICameraView camera;

        private readonly ITowerTargetRayView rayView;

        private ITowerView interactingTower;

        public bool IsInteracting => interactingTower != null;

        [Inject]
        public TowerInteractionAction(ITowerInteractionActionView view,
            ITowerInteractionActionUi ui, ITowerTargetRayView rayView, ICameraService cameraService,
            Scene scene) : base(view, ui, scene)
        {
            this.rayView = rayView;
            camera = cameraService.MainCamera;
        }

        public override void OnSceneStarted()
        {
            base.OnSceneStarted();

            Ui.SingleBranchUpgradeButtonClicked += UpgradeBranch;

            Ui.FirstBranchUpgradeButtonClicked += UpgradeFirstBranch;
            Ui.SecondBranchUpgradeButtonClicked += UpgradeSecondBranch;
        }

        public override void OnSceneFinished()
        {
            base.OnSceneFinished();

            Ui.SingleBranchUpgradeButtonClicked -= UpgradeBranch;

            Ui.FirstBranchUpgradeButtonClicked -= UpgradeFirstBranch;
            Ui.SecondBranchUpgradeButtonClicked -= UpgradeSecondBranch;
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

            interactingTower.UpgradeFirstBranch();

            Ui.InteractingTower = interactingTower;
        }

        public void UpgradeSecondBranch()
        {
            if (!IsInteracting)
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
