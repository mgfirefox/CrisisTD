using System.Runtime.CompilerServices;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public static class Vector3Utility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 GetTranslation(Vector3 a, Vector3 b)
        {
            return b - a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetSqrDistance(Vector3 a, Vector3 b)
        {
            return GetTranslation(a, b).sqrMagnitude;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 GetDirection(Vector3 a, Vector3 b)
        {
            return GetTranslation(a, b).normalized;
        }

        public static bool IsPointOnLineSegment(Vector3 p, Vector3 a, Vector3 b, float epsilon)
        {
            Vector3 ab = GetTranslation(a, b);

            Vector3 ap = GetTranslation(a, p);
            if (!ap.HasSameDirection(ab, epsilon))
            {
                return false;
            }

            Vector3 pb = GetTranslation(p, b);
            if (!pb.HasSameDirection(ab, epsilon))
            {
                return false;
            }

            return true;
        }

        public static Vector3 ProjectPointOnLineSegment(Vector3 p, Vector3 a, Vector3 b,
            float epsilon)
        {
            Vector3 ab = GetTranslation(a, b);
            if (ab.IsApproximateZero(epsilon))
            {
                return a;
            }

            Vector3 ap = GetTranslation(a, p);

            float t = Vector3.Dot(ap, ab) / ab.sqrMagnitude;
            float clampedT = Mathf.Clamp01(t);

            return a + ab * clampedT;
        }
    }
}
