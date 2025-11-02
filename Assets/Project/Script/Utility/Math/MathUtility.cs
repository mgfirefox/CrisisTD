using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public static class MathUtility
    {
        public static Vector3 GetQuadraticBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            float u = 1.0f - t;

            return u * u * p0 + 2.0f * u * t * p1 + t * t * p2;
        }

        public static Vector3 GetCubicBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3,
            float t)
        {
            float u = 1.0f - t;

            return u * u * u * p0 + 3.0f * u * u * t * p1 + 3.0f * u * t * t * p2 + t * t * t * p3;
        }
    }
}
