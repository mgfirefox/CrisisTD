using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class TowerInteractionAction :
        AbstractAction<TowerInteractionActionData, ITowerInteractionActionView>,
        ITowerInteractionAction
    {
        private readonly ICameraView camera;

        private readonly ITowerTargetRayView rayView;

        private ITowerView interactingTower;

        public bool IsInteracting => interactingTower != null;

        [Inject]
        public TowerInteractionAction(ITowerInteractionActionView view, ITowerTargetRayView rayView,
            ICameraService cameraService, Scene scene) : base(view, scene)
        {
            this.rayView = rayView;
            camera = cameraService.MainCamera;
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

                return;
            }

            Cancel();
        }

        public void UpgradeBranch1()
        {
            if (!IsInteracting)
            {
                return;
            }

            interactingTower.UpgradeBranch1();
        }

        public void UpgradeBranch2()
        {
            if (!IsInteracting)
            {
                return;
            }

            interactingTower.UpgradeBranch2();
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
        }

        protected override void OnInitialized(TowerInteractionActionData data)
        {
            base.OnInitialized(data);

            View.IsInteracting = IsInteracting;

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
