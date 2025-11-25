using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class TowerPlacementAction :
        AbstractUiAction<TowerPlacementActionData, ITowerPlacementActionView,
            ITowerPlacementActionUi>, ITowerPlacementAction
    {
        private readonly ITowerPreviewFactory towerPreviewFactory;
        private readonly ITowerObstacleViewFactory towerObstacleViewFactory;

        private readonly IRotationService rotationService;

        private readonly ITowerService towerService;

        private readonly IMapService mapService;

        private readonly ICameraView camera;

        private readonly IBasicRayView rayView;

        private readonly ILoadoutService loadoutService;

        private ITowerPreviewView placingTowerPreviewView;
        private int placingTowerPreviewViewYaw;

        private readonly ISet<ITowerView> placedTowers = new HashSet<ITowerView>();
        private readonly IDictionary<ITowerView, Action> placedTowerDestroyingActions =
            new Dictionary<ITowerView, Action>();

        private readonly IDictionary<ITowerView, ITowerObstacleView> towerObstacles =
            new Dictionary<ITowerView, ITowerObstacleView>();
        private readonly IList<ITowerObstacleView> routeSegmentTowerObstacles =
            new List<ITowerObstacleView>();

        public int SelectedIndex { get; private set; } = -1;

        public int Limit { get; private set; }
        public int Count { get; private set; }

        public bool IsPlacing => SelectedIndex != -1;
        public bool IsPlacementSuitable { get; private set; }

        public bool IsLimitReached => Count >= Limit;

        private bool HasPlacingTowerPreviewSameYawStep(float yaw)
        {
            int placingTowerYawStep =
                Math.Abs(placingTowerPreviewViewYaw / Constant.towerPlacementYawStepValue);
            placingTowerYawStep %= 2;

            int yawStep = Mathf.RoundToInt(yaw / Constant.towerPlacementYawStepValue);
            yawStep = Math.Abs(yawStep) % 2;

            if (placingTowerYawStep == yawStep)
            {
                return true;
            }

            return false;
        }

        private bool HasPlacingTowerPreviewSameYawStep(Quaternion orientation)
        {
            float yaw = orientation.eulerAngles.y;

            return HasPlacingTowerPreviewSameYawStep(yaw);
        }

        [Inject]
        public TowerPlacementAction(ITowerPlacementActionView view, ITowerPlacementActionUi ui,
            ITowerService towerService, IMapService mapService, ILoadoutService loadoutService,
            IBasicRayView rayView, ICameraService cameraService,
            ITowerPreviewFactory towerPreviewFactory,
            ITowerObstacleViewFactory towerObstacleViewFactory, IRotationService rotationService,
            Scene scene) : base(view, ui, scene)
        {
            this.towerService = towerService;
            this.mapService = mapService;
            this.loadoutService = loadoutService;
            this.rayView = rayView;
            camera = cameraService.MainCamera;
            this.towerPreviewFactory = towerPreviewFactory;
            this.towerObstacleViewFactory = towerObstacleViewFactory;
            this.rotationService = rotationService;
        }

        public override void Perform()
        {
            if (!IsPlacing)
            {
                return;
            }

            if (!IsPlacementSuitable)
            {
                return;
            }

            if (!IsLimitReached)
            {
                LoadoutItem loadoutItem = loadoutService.GetItem(SelectedIndex);

                if (towerService.TrySpawn(loadoutItem.TowerId, placingTowerPreviewView.Position,
                        Quaternion.identity, out ITowerView tower))
                {
                    tower.ObstacleOrientation = placingTowerPreviewView.Orientation;
                    
                    Action towerDestroyAction = () => OnPlacedTowerDestroying(tower);

                    tower.Destroying += towerDestroyAction;

                    placedTowers.Add(tower);
                    placedTowerDestroyingActions[tower] = towerDestroyAction;

                    Count++;

                    View.Count = Count;
                    View.IsLimitReached = IsLimitReached;

                    Ui.Count = Count;

                    Deselect();

                    return;
                }

                // TODO: Change Error
                Debug.LogError("Failed to spawn tower.");
            }
        }

        public void Select(int index)
        {
            if (index < -1 || index >= loadoutService.Count)
            {
                throw new InvalidArgumentException(nameof(index), index.ToString());
            }
            if (index == SelectedIndex)
            {
                return;
            }

            if (IsPlacing)
            {
                Deselect();
            }

            LoadoutItem loadoutItem = loadoutService.GetItem(index);

            if (towerPreviewFactory.TryCreate(loadoutItem.TowerId, out placingTowerPreviewView))
            {
                placingTowerPreviewView.ObstacleSize = new Vector3(Constant.towerObstacleLength,  Constant.epsilon, Constant.towerObstacleWidth);
                placingTowerPreviewView.Hide();
            }
            else
            {
                // TODO: Change Warning
                Debug.LogWarning($"Failed to create Object of type {typeof(ITowerPreviewView)}");

                return;
            }

            SelectedIndex = index;
            IsPlacementSuitable = false;

            View.SelectedIndex = SelectedIndex;
            View.IsPlacing = IsPlacing;
            View.IsPlacementSuitable = IsPlacementSuitable;

            CreateTowerObstacles();
            CreateRouteSegmentTowerObstacles();
        }

        public void Deselect()
        {
            if (!IsPlacing)
            {
                return;
            }

            SelectedIndex = -1;
            IsPlacementSuitable = false;

            View.SelectedIndex = SelectedIndex;
            View.IsPlacing = IsPlacing;
            View.IsPlacementSuitable = IsPlacementSuitable;

            placingTowerPreviewView.Destroy();
            placingTowerPreviewView = null;
            placingTowerPreviewViewYaw = 0;

            DestroyTowerObstacles();
            DestroyRouteSegmentTowerObstacles();
        }

        public void Rotate()
        {
            if (!IsPlacing)
            {
                return;
            }

            placingTowerPreviewViewYaw += Constant.towerPlacementYawStepValue;

            Vector3 eulerAngles = placingTowerPreviewView.Orientation.eulerAngles;
            eulerAngles.y = placingTowerPreviewViewYaw;

            placingTowerPreviewView.Orientation = Quaternion.Euler(eulerAngles);

            DestroyTowerObstacles();
            CreateTowerObstacles();
        }

        public void UpdatePreview()
        {
            if (!IsPlacing)
            {
                return;
            }

            float epsilon = Scene.Settings.MathSettings.Epsilon;

            Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();

            Ray cursorRay = camera.GetRayFromScreenPosition(mouseScreenPosition);

            rayView.SetNormalizedRay(cursorRay.origin, cursorRay.direction);

            if (TryGetRayHit(out RayHit cursorHit, out bool isHitSuitable))
            {
                RayHit suitableHit;

                if (isHitSuitable)
                {
                    suitableHit = cursorHit;
                }
                else
                {
                    Debug.DrawRay(rayView.StartPosition, rayView.Direction * cursorHit.Distance,
                        Color.red);

                    Vector3 direction =
                        Vector3.Project(cursorHit.Normal, Vector3.forward).normalized;
                    Vector3 translation = direction * Constant.epsilon;

                    rayView.SetNormalizedRay(cursorHit.Position + translation, Vector3.down);

                    if (TryGetRayHit(out RayHit downHit, out bool isDownHitSuitable))
                    {
                        if (isDownHitSuitable)
                        {
                            suitableHit = downHit;
                        }
                        else
                        {
                            IsPlacementSuitable = false;

                            View.IsPlacementSuitable = IsPlacementSuitable;

                            placingTowerPreviewView.Position = downHit.Position;
                            placingTowerPreviewView.Show();

                            Debug.DrawRay(rayView.StartPosition,
                                rayView.Direction * downHit.Distance, Color.red);

                            return;
                        }
                    }
                    else
                    {
                        IsPlacementSuitable = false;

                        View.IsPlacementSuitable = IsPlacementSuitable;

                        placingTowerPreviewView.Hide();

                        Debug.DrawLine(rayView.StartPosition, rayView.EndPosition, Color.red);

                        return;
                    }
                }

                foreach (ITowerObstacleView towerObstacle in towerObstacles.Values)
                {
                    if (!towerObstacle.IsPositionWithin(suitableHit.Position, epsilon))
                    {
                        continue;
                    }

                    IsPlacementSuitable = false;

                    View.IsPlacementSuitable = IsPlacementSuitable;

                    placingTowerPreviewView.Position = suitableHit.Position;
                    placingTowerPreviewView.Show();

                    Debug.DrawLine(rayView.StartPosition, suitableHit.Position, Color.red);

                    return;
                }
                foreach (ITowerObstacleView routeSegmentTowerObstacle in routeSegmentTowerObstacles)
                {
                    if (!routeSegmentTowerObstacle.IsPositionWithin(suitableHit.Position, epsilon))
                    {
                        continue;
                    }

                    IsPlacementSuitable = false;

                    View.IsPlacementSuitable = IsPlacementSuitable;

                    placingTowerPreviewView.Position = suitableHit.Position;
                    placingTowerPreviewView.Show();

                    Debug.DrawLine(rayView.StartPosition, suitableHit.Position, Color.red);

                    return;
                }

                IsPlacementSuitable = true;

                View.IsPlacementSuitable = IsPlacementSuitable;

                placingTowerPreviewView.Position = suitableHit.Position;
                placingTowerPreviewView.Show();

                Debug.DrawLine(rayView.StartPosition, suitableHit.Position, Color.green);
            }
            else
            {
                IsPlacementSuitable = false;

                View.IsPlacementSuitable = IsPlacementSuitable;

                placingTowerPreviewView.Hide();

                Debug.DrawLine(rayView.StartPosition, rayView.EndPosition, Color.red);
            }
        }

        private void CreateTowerObstacles()
        {
            foreach (ITowerView placedTower in placedTowers)
            {
                if (towerObstacleViewFactory.TryCreate(out ITowerObstacleView towerObstacle))
                {
                    float length;
                    float width;
                    if (HasPlacingTowerPreviewSameYawStep(placedTower.ObstacleOrientation))
                    {
                        length = GetSizeParameter(Constant.towerObstacleLength,
                            Constant.towerObstacleLength);
                        width = GetSizeParameter(Constant.towerObstacleWidth,
                            Constant.towerObstacleWidth);
                    }
                    else
                    {
                        length = GetSizeParameter(Constant.towerObstacleLength,
                            Constant.towerObstacleWidth);
                        width = GetSizeParameter(Constant.towerObstacleWidth,
                            Constant.towerObstacleLength);
                    }

                    towerObstacle.Position = placedTower.Position;
                    towerObstacle.Orientation = placedTower.ObstacleOrientation;
                    towerObstacle.Size = new Vector3(length, Constant.towerObstacleHeight, width);

                    towerObstacles[placedTower] = towerObstacle;
                }
                else
                {
                    // TODO: Change Warning
                    Debug.LogWarning(
                        $"Failed to create Object of type {typeof(ITowerObstacleView)}");
                }
            }
        }

        private void CreateRouteSegmentTowerObstacles()
        {
            IReadOnlyList<Vector3> waypoints = mapService.Waypoints;
            for (int i = 0; i < waypoints.Count - 1; i++)
            {
                Vector3 waypoint = waypoints[i];
                Vector3 nextWaypoint = waypoints[i + 1];

                Vector3 direction = Vector3Utility.GetTranslation(waypoint, nextWaypoint);
                float segmentWidth = direction.magnitude;
                direction.Normalize();

                Vector3 translation = direction * segmentWidth / 2;

                Vector3 segmentPosition = waypoint + translation;
                Quaternion segmentOrientation = rotationService.RotateTo(waypoint, nextWaypoint);

                if (towerObstacleViewFactory.TryCreate(out ITowerObstacleView towerObstacle))
                {
                    towerObstacle.Position = segmentPosition;
                    towerObstacle.Orientation = segmentOrientation;
                    towerObstacle.Size = new Vector3(Constant.routeSegmentTowerObstacleLength,
                        Constant.routeSegmentTowerObstacleHeight,
                        Constant.routeSegmentTowerObstacleWidth + segmentWidth);

                    routeSegmentTowerObstacles.Add(towerObstacle);
                }
                else
                {
                    // TODO: Change Warning
                    Debug.LogWarning(
                        $"Failed to create Object of type {typeof(ITowerObstacleView)}");
                }
            }
        }

        private void DestroyTowerObstacles()
        {
            foreach (ITowerObstacleView towerObstacle in towerObstacles.Values)
            {
                towerObstacle.Destroy();
            }
            towerObstacles.Clear();
        }

        private void DestroyRouteSegmentTowerObstacles()
        {
            foreach (ITowerObstacleView routeSegmentObstacle in routeSegmentTowerObstacles)
            {
                routeSegmentObstacle.Destroy();
            }
            routeSegmentTowerObstacles.Clear();
        }

        private bool TryGetRayHit(out RayHit hit, out bool isHitSuitable)
        {
            float epsilon = Scene.Settings.MathSettings.Epsilon;

            rayView.CollisionLayerMask = Constant.TowerPlacementSuitableLayerMask;
            if (rayView.TryCast(out RayHit suitableHit))
            {
                isHitSuitable = true;
            }
            else
            {
                isHitSuitable = false;
            }

            rayView.CollisionLayerMask = Constant.TowerPlacementNonSuitableLayerMask;
            if (rayView.TryCast(out RayHit nonSuitableHit))
            {
                if (!isHitSuitable || Vector3Utility
                        .GetSqrDistance(rayView.StartPosition, suitableHit.Position)
                        .IsGreaterThanApproximately(
                            Vector3Utility.GetSqrDistance(rayView.StartPosition,
                                nonSuitableHit.Position), epsilon))
                {
                    hit = nonSuitableHit.Clone() as RayHit;

                    isHitSuitable = false;

                    return true;
                }
            }
            else
            {
                if (!isHitSuitable)
                {
                    hit = null;

                    return false;
                }
            }

            hit = suitableHit.Clone() as RayHit;

            isHitSuitable = true;

            return true;
        }

        private float GetSizeParameter(float initialSizeParameter, float placingTowerSizeParameter)
        {
            float epsilon = Scene.Settings.MathSettings.Epsilon;

            if (initialSizeParameter.IsLessThanApproximately(placingTowerSizeParameter, epsilon))
            {
                return placingTowerSizeParameter;
            }

            return initialSizeParameter;
        }

        protected override void OnInitialized(TowerPlacementActionData data)
        {
            base.OnInitialized(data);

            SelectedIndex = data.SelectedIndex;
            Limit = data.Limit;
            Count = data.Count;
            IsPlacementSuitable = false;

            Select(SelectedIndex);

            View.SelectedIndex = SelectedIndex;
            View.Limit = Limit;
            View.Count = Count;
            View.IsPlacing = IsPlacing;
            View.IsPlacementSuitable = IsPlacementSuitable;
            View.IsLimitReached = IsLimitReached;

            Ui.Limit = Limit;
            Ui.Count = Count;

            rayView.Initialize();

            rayView.MaxDistance = Constant.towerPlacementMaxDistance;
        }

        protected override void OnDestroying()
        {
            DestroyTowerObstacles();

            Deselect();

            rayView.Destroy();

            base.OnDestroying();
        }

        private void OnPlacedTowerDestroying(ITowerView placedTower)
        {
            placedTower.Destroying -= placedTowerDestroyingActions[placedTower];

            Count--;

            View.Count = Count;

            Ui.Count = Count;

            placedTowers.Remove(placedTower);
            placedTowerDestroyingActions.Remove(placedTower);

            if (towerObstacles.TryGetValue(placedTower, out ITowerObstacleView towerObstacle))
            {
                towerObstacle.Destroy();
                towerObstacles.Remove(placedTower);
            }
        }
    }
}
