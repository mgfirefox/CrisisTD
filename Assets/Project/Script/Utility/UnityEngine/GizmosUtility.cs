using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public static class GizmosUtility
    {
        public static void DrawBezierCurve(Vector3 startPosition, Vector3 endPosition,
            Vector3[] controlPointPositions = null, int segmentCount = 8)
        {
            if (segmentCount <= 0)
            {
                return;
            }

            if (controlPointPositions == null || controlPointPositions.Length == 0)
            {
                Gizmos.DrawLine(startPosition, endPosition);

                return;
            }

            Vector3 previousPointPosition = startPosition;

            float step = 1.0f / segmentCount;
            for (int i = 1; i <= segmentCount; i++)
            {
                float t = i * step;

                Vector3 currentPointPosition;

                switch (controlPointPositions.Length)
                {
                    case 1:
                        currentPointPosition = MathUtility.GetQuadraticBezierPoint(startPosition,
                            controlPointPositions[0], endPosition, t);
                    break;
                    case 2:
                        currentPointPosition = MathUtility.GetCubicBezierPoint(startPosition,
                            controlPointPositions[0], controlPointPositions[1], endPosition, t);
                    break;
                    default:
                        Debug.LogWarning(
                            $"Unsupported control point count ({controlPointPositions.Length}) for drawing Bezier curve.");
                        return;
                }

                Gizmos.DrawLine(previousPointPosition, currentPointPosition);

                previousPointPosition = currentPointPosition;
            }
        }
    }
}
