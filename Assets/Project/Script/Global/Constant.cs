using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public static class Constant
    {
        public const float epsilon = 0.001f;

        public const float rangeHeight = 10.0f;

        public const int towerLoadoutSize = 5;

        public const float towerPlacementMaxDistance = 100.0f;
        public static LayerMask TowerPlacementSuitableLayerMask { get; } = LayerMask.GetMask("Map");
        public static LayerMask TowerPlacementNonSuitableLayerMask { get; } =
            LayerMask.GetMask("MapObstacle");
        public const int towerPlacementYawStepValue = 90;

        public const float towerObstacleLength = 10.0f;
        public const float towerObstacleHeight = rangeHeight;
        public const float towerObstacleWidth = 20.0f;

        public const float routeSegmentTowerObstacleLength = 5.0f;
        public const float routeSegmentTowerObstacleHeight = rangeHeight;
        public const float routeSegmentTowerObstacleWidth = routeSegmentTowerObstacleLength;

        public const int maxHitboxTargetCount = 64;
        public const int maxRayCastHitTargetCount = 64;

        public const float minEffectValue = 0.0f;
        public const float maxEffectValue = float.MaxValue;
        public const float minBuffEffectValue = 0.0f;
        public const float maxBuffEffectValue = float.MaxValue;
        public const float minDebuffEffectValue = 0.0f;
        public const float maxDebuffEffectValue = 1.0f;

        public const float towerInteractionMaxDistance = 100.0f;
        public static LayerMask TowerSelectionLayerMask { get; } = LayerMask.GetMask("Tower");
    }
}
