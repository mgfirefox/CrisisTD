using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class SupportTowerActionUi : AbstractTowerActionUi<ISupportTowerActionView>,
        ISupportTowerActionUi
    {
        private int selectedActionIndex;

        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private BuffActionUiFolder actionUiFolder;

        public override ISupportTowerActionView View
        {
            set
            {
                base.View = value;

                SetAction(value.ActionFolder);
            }
        }

        private void SetAction(IBuffActionFolder folder)
        {
            IReadOnlyList<IBuffActionView> folderChildren = folder.Children;
            if (folderChildren.Count == 0)
            {
                return;
            }

            IBuffActionView action = folderChildren[selectedActionIndex];
            switch (action)
            {
                case IConstantBuffActionView constantAction:
                    foreach (IBuffActionUi actionUi in actionUiFolder.Children)
                    {
                        if (actionUi is IConstantBuffActionUi constantActionUi)
                        {
                            constantActionUi.View = constantAction;
                            constantActionUi.Show();

                            continue;
                        }

                        actionUi.Hide();
                    }
                break;
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            actionUiFolder.Initialize();
        }

        protected override void OnDestroying()
        {
            actionUiFolder.Destroy();

            base.OnDestroying();
        }
    }
}
