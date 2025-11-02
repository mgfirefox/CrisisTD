using System.Runtime.CompilerServices;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public static class FloatExtension
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EqualsApproximately(this float self, float other, float epsilon)
        {
            return Mathf.Abs(self - other) <= epsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsApproximateZero(this float self, float epsilon)
        {
            return EqualsApproximately(self, 0.0f, epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsGreaterThanApproximately(this float self, float other, float epsilon)
        {
            return self > other + epsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLessThanApproximately(this float self, float other, float epsilon)
        {
            return self < other - epsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsGreaterThanOrEqualApproximately(this float self, float other,
            float epsilon)
        {
            return !IsLessThanApproximately(self, other, epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLessThanOrEqualApproximately(this float self, float other,
            float epsilon)
        {
            return !IsGreaterThanApproximately(self, other, epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsApproximatePositive(this float self, float epsilon)
        {
            return IsGreaterThanApproximately(self, 0.0f, epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsApproximateNegative(this float self, float epsilon)
        {
            return IsLessThanApproximately(self, 0.0f, epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsApproximateNonPositive(this float self, float epsilon)
        {
            return IsLessThanOrEqualApproximately(self, 0.0f, epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsApproximateNonNegative(this float self, float epsilon)
        {
            return IsGreaterThanOrEqualApproximately(self, 0.0f, epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Normalize(this float self)
        {
            return Mathf.Sign(self);
        }
    }
}
