using System.Runtime.CompilerServices;
using VContainer.Unity;

namespace Mgfirefox.CrisisTd
{
    public static class LifetimeScopeExtension
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TScope CreateChildFromPrefabRespectSettings<TScope>(this LifetimeScope self,
            TScope prefab, IInstaller installer = null)
            where TScope : LifetimeScope
        {
            TScope child = self.CreateChildFromPrefab(prefab, installer);

            var settings = VContainerSettings.Instance;
            if (settings != null && settings.RemoveClonePostfix)
            {
                child.name = child.name.Replace("(Clone)", "").TrimEnd();
            }

            return child;
        }
    }
}
