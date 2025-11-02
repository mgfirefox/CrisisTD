using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class AttackTowerActionView : AbstractTowerActionView, IAttackTowerActionView
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private AttackActionFolderView actionViewFolder;

        public IAttackActionFolderView ActionViewFolder => actionViewFolder;

        protected override IUnitySceneObject GetChildParent(IUnitySceneObject child)
        {
            if (child.Transform.TryGetComponent(out IAttackActionView _))
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
