using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace Mgfirefox.CrisisTd
{
    public class SupportTowerActionView : AbstractTowerActionView, ISupportTowerActionView
    {
        [FormerlySerializedAs("actionViewFolder")]
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private BuffActionFolder actionFolder;

        public IBuffActionFolder ActionFolder => actionFolder;

        protected override IUnitySceneObject GetChildParent(IUnitySceneObject child)
        {
            if (child.Transform.TryGetComponent(out IBuffActionView _))
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
