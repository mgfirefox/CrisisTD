using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class AttackTowerActionView : AbstractTowerActionView, IAttackTowerActionView
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private AttackActionFolder actionFolder;

        public IAttackActionFolder ActionFolder => actionFolder;

        protected override IUnitySceneObject GetChildParent(IUnitySceneObject child)
        {
            if (child.Transform.TryGetComponent(out IAttackActionView _))
            {
                return actionFolder;
            }

            return base.GetChildParent(child);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            actionFolder.Initialize();
        }

        protected override void OnDestroying()
        {
            actionFolder.Destroy();

            base.OnDestroying();
        }
    }
}
