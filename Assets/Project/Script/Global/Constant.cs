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
        public static LayerMask TowerPlacementNonSuitable1LayerMask { get; } =
            LayerMask.GetMask("TowerObstacle");

        public const float towerObstacleLength = 5.0f;
        public const float towerObstacleHeight = rangeHeight;
        public const float towerObstacleWidth = towerObstacleLength;

        public const float waypointSegmentTowerObstacleHeight = rangeHeight;
        public const float waypointSegmentTowerObstacleWidth = 2.0f;

        public const int maxHitboxTargetCount = 64;
        public const int maxRayCastHitTargetCount = 64;

        public const float minEffectValue = 0.0f;
        public const float maxEffectValue = float.MaxValue;
        public const float minBuffEffectValue = 0.0f;
        public const float maxBuffEffectValue = float.MaxValue;
        public const float minDebuffEffectValue = 0.0f;
        public const float maxDebuffEffectValue = 1.0f;

        public const int maxLevelBranch0Index = 3;
        public const int maxLevelBranch1Index = 3;
        public const int maxLevelBranch2Index = 3;

        public const float towerInteractionMaxDistance = 100.0f;
        public static LayerMask TowerSelectionLayerMask { get; } = LayerMask.GetMask("Tower");
    }
}
