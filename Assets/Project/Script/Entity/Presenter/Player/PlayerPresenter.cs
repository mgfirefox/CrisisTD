using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class PlayerPresenter : AbstractPresenter<PlayerData, IPlayerView>, IPlayerPresenter,
        ISceneTickedListener
    {
        private readonly ITowerPlacementActionFactory towerPlacementActionFactory;
        private readonly ITowerInteractionActionFactory towerInteractionActionFactory;

        private readonly ICameraService cameraService;

        private readonly IPlayerTransformService transformService;

        private readonly ILoadoutService loadoutService;
        private ITowerPlacementAction towerPlacementAction;

        private ITowerInteractionAction towerInteractionAction;

        private readonly InputActions inputActions;

        private readonly ITowerPlacementActionUi towerPlacementActionUi;

        [Inject]
        public PlayerPresenter(IPlayerView view, IPlayerTransformService transformService,
            ILoadoutService loadoutService, ICameraService cameraService, InputActions inputActions,
            ITowerPlacementActionUi towerPlacementActionUi,
            ITowerPlacementActionFactory towerPlacementActionFactory,
            ITowerInteractionActionFactory towerInteractionActionFactory, Scene scene) : base(view,
            scene)
        {
            this.transformService = transformService;
            this.loadoutService = loadoutService;
            this.cameraService = cameraService;
            this.towerPlacementActionFactory = towerPlacementActionFactory;
            this.towerInteractionActionFactory = towerInteractionActionFactory;
            this.inputActions = inputActions;
            this.towerPlacementActionUi = towerPlacementActionUi;
        }

        public override void OnSceneStarted()
        {
            base.OnSceneStarted();

            cameraService.AddVirtualCamera(View.IsometricVirtualCamera, CameraPriority.Isometric);
            cameraService.AddVirtualCamera(View.TopDownVirtualCamera, CameraPriority.TopDown);

            cameraService.EnableVirtualCamera(View.IsometricVirtualCamera);
            cameraService.MainCamera.Initialize();

            inputActions.Player.Enable();
            inputActions.TowerPlacement.Enable();
            inputActions.TowerInteraction.Enable();

            towerPlacementActionUi.TowerButtonClicked += OnTowerButtonClicked;
        }

        public override void OnSceneFinished()
        {
            base.OnSceneFinished();

            cameraService.RemoveVirtualCamera(View.IsometricVirtualCamera);
            cameraService.RemoveVirtualCamera(View.TopDownVirtualCamera);

            inputActions.Player.Disable();
            inputActions.TowerPlacement.Disable();
            inputActions.TowerInteraction.Disable();

            towerPlacementActionUi.TowerButtonClicked -= OnTowerButtonClicked;
        }

        public void OnSceneTicked()
        {
            SwitchCamera();
            Move();

            PlaceTower();

            InteractWithTower();
        }

        private void SwitchCamera()
        {
            InputAction switchCameraInputAction = inputActions.Player.SwitchCamera;
            if (!switchCameraInputAction.WasReleasedThisFrame())
            {
                return;
            }

            if (cameraService.IsVirtualCameraEnabled(View.TopDownVirtualCamera))
            {
                cameraService.DisableVirtualCamera(View.TopDownVirtualCamera);
            }
            else
            {
                cameraService.EnableVirtualCamera(View.TopDownVirtualCamera);
            }
        }

        private void Move()
        {
            InputAction moveInputAction = inputActions.Player.Move;
            if (!moveInputAction.IsPressed())
            {
                return;
            }

            var translationDirection = moveInputAction.ReadValue<Vector2>();

            transformService.Move(translationDirection);

            View.Position = transformService.Position;
            View.Orientation = transformService.Orientation;
            View.MovementSpeed = transformService.MovementSpeed;
        }

        private void PlaceTower()
        {
            if (towerPlacementAction == null)
            {
                return;
            }

            InputAction selectInputAction = inputActions.TowerPlacement.Select;
            if (selectInputAction.WasPressedThisFrame())
            {
                int index = Mathf.RoundToInt(selectInputAction.ReadValue<float>());
                towerPlacementAction.Select(index);
            }

            if (!towerPlacementAction.IsPlacing)
            {
                return;
            }

            if (towerInteractionAction.IsInteracting)
            {
                towerInteractionAction.Cancel();
            }

            InputAction deselectInputAction = inputActions.TowerPlacement.Deselect;
            if (deselectInputAction.WasReleasedThisFrame())
            {
                towerPlacementAction.Deselect();

                return;
            }

            InputAction rotateInputAction = inputActions.TowerPlacement.Rotate;
            if (rotateInputAction.WasReleasedThisFrame())
            {
                towerPlacementAction.Rotate();
            }

            InputAction placeInputAction = inputActions.TowerPlacement.Place;
            if (placeInputAction.WasReleasedThisFrame())
            {
                towerPlacementAction.Perform();

                return;
            }

            towerPlacementAction.UpdatePreview();
        }

        private void InteractWithTower()
        {
            if (towerPlacementAction?.IsPlacing ?? false)
            {
                return;
            }

            if (towerInteractionAction == null)
            {
                return;
            }

            InputAction interactInputAction = inputActions.TowerInteraction.Interact;
            if (interactInputAction.WasReleasedThisFrame())
            {
                towerInteractionAction.Perform();
            }

            if (!towerInteractionAction.IsInteracting)
            {
                return;
            }

            InputAction cancelInputAction = inputActions.TowerInteraction.Cancel;
            if (cancelInputAction.WasReleasedThisFrame())
            {
                towerInteractionAction.Cancel();

                return;
            }

            InputAction upgradeInputAction = inputActions.TowerInteraction.Upgrade;
            if (upgradeInputAction.WasPressedThisFrame())
            {
                int option = Mathf.RoundToInt(upgradeInputAction.ReadValue<float>());
                switch (option)
                {
                    case 0:
                        towerInteractionAction.UpgradeFirstBranch();
                    break;
                    case 1:
                        towerInteractionAction.UpgradeSecondBranch();
                    break;
                }
            }

            InputAction sellInputAction = inputActions.TowerInteraction.Sell;
            if (sellInputAction.WasReleasedThisFrame())
            {
                towerInteractionAction.Sell();
            }
        }

        protected override void OnInitialized(PlayerData data)
        {
            base.OnInitialized(data);

            transformService.Initialize(data.TransformServiceData);

            View.Position = transformService.Position;
            View.Orientation = transformService.Orientation;
            View.MaxMovementSpeed = transformService.MaxMovementSpeed;
            View.MovementSpeed = transformService.MovementSpeed;

            loadoutService.Initialize(data.LoadoutServiceData);

            View.Loadout = loadoutService.Loadout;

            InitializeTowerPlacementAction(data.TowerPlacementActionData);
            InitializeTowerInteractionAction(data.TowerInteractionActionData);
        }

        private void InitializeTowerPlacementAction(TowerPlacementActionData data)
        {
            if (towerPlacementActionFactory.TryCreate(data, View, out towerPlacementAction))
            {
            }
            else
            {
                // TODO: Change Warning
                Debug.LogWarning(
                    $"Failed to create Object of type {typeof(ITowerPlacementAction)}");
            }
        }

        private void InitializeTowerInteractionAction(TowerInteractionActionData data)
        {
            if (towerInteractionActionFactory.TryCreate(data, View, out towerInteractionAction))
            {
            }
            else
            {
                // TODO: Change Warning
                Debug.LogWarning(
                    $"Failed to create Object of type {typeof(ITowerPlacementAction)}");
            }
        }

        protected override void OnDestroying()
        {
            DestroyTowerInteractionAction();
            DestroyTowerPlacementAction();

            loadoutService.Destroy();

            transformService.Destroy();

            base.OnDestroying();
        }

        private void DestroyTowerInteractionAction()
        {
            towerInteractionAction?.Destroy();
        }

        private void DestroyTowerPlacementAction()
        {
            towerPlacementAction?.Destroy();
        }

        private void OnTowerButtonClicked(int index)
        {
            if (towerInteractionAction != null && towerInteractionAction.IsInteracting)
            {
                towerInteractionAction.Cancel();
            }

            towerPlacementAction?.Select(index);
        }
    }
}
