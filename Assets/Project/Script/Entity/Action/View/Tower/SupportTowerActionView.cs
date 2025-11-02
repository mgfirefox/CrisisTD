using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class SupportTowerActionView : AbstractTowerActionView, ISupportTowerActionView
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private BuffActionFolderView actionViewFolder;

        public IBuffActionFolderView ActionViewFolder => actionViewFolder;

        protected override IUnitySceneObject GetChildParent(IUnitySceneObject child)
        {
            if (child.Transform.TryGetComponent(out IBuffActionView _))
            {
                return actionViewFolder;
            }

            return base.GetChildParent(child);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            actionViewFolder.Initialize();
        }

        protected override void OnDestroying()
        {
            actionViewFolder.Destroy();

            base.OnDestroying();
        }
    }
}
