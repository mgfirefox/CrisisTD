using System.Runtime.CompilerServices;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public static class Vector3Extension
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EqualsApproximately(this Vector3 self, Vector3 other, float epsilon)
        {
            Vector3 translation = Vector3Utility.GetTranslation(self, other);

            return translation.IsApproximateZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsApproximateZero(this Vector3 self, float epsilon)
        {
            return MagnitudeEqualsApproximately(self, Vector3.zero, epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool MagnitudeEqualsApproximately(this Vector3 self, Vector3 other,
            float epsilon)
        {
            return Vector3Utility.GetSqrDistance(self, other).IsApproximateZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsCollinear(this Vector3 self, Vector3 other, float epsilon)
        {
            if (self.IsApproximateZero(epsilon) || other.IsApproximateZero(epsilon))
            {
                return false;
            }

            if (!Vector3.Cross(self, other).IsApproximateZero(epsilon))
            {
                return false;
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasSameDirection(this Vector3 self, Vector3 other, float epsilon)
        {
            if (self.IsApproximateZero(epsilon) || other.IsApproximateZero(epsilon))
            {
                return true;
            }

            if (self.EqualsApproximately(other, epsilon))
            {
                return true;
            }

            if (!self.IsCollinear(other, epsilon))
            {
                return false;
            }

            if (Vector3.Dot(self, other).IsApproximateNegative(epsilon))
            {
                return false;
            }

            return true;
        }
    }
}
