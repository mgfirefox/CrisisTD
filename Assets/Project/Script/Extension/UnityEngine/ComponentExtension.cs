using System.Runtime.CompilerServices;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public static class ComponentExtension
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetComponentInChildren<TInterface>(this Component self,
            out TInterface component, bool includeInactive = false)
            where TInterface : class
        {
            component = self.GetComponentInChildren<TInterface>(includeInactive);

            return component != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetComponentInParent<TInterface>(this Component self,
            out TInterface component)
            where TInterface : class
        {
            component = self.GetComponentInParent<TInterface>();

            return component != null;
        }
    }
}
