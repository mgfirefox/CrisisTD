using System.Runtime.CompilerServices;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public static class LayerMaskExtension
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains(this LayerMask self, int layer)
        {
            return (self.value & (1 << layer)) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LayerMask Add(this LayerMask self, int layer)
        {
            self.value |= 1 << layer;
            return self;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LayerMask Remove(this LayerMask self, int layer)
        {
            self.value &= ~(1 << layer);
            return self;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Overlaps(this LayerMask self, LayerMask other)
        {
            return (self.value & other.value) != 0;
        }
    }
}
