using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class AttackTowerActionUi : AbstractTowerActionUi<IAttackTowerActionView>,
        IAttackTowerActionUi
    {
        private int selectedActionIndex;

        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private AttackActionUiFolder actionUiFolder;

        public override IAttackTowerActionView View
        {
            set
            {
                base.View = value;

                SetAction(value.ActionFolder);
            }
        }

        private void SetAction(IAttackActionFolder folder)
        {
            IReadOnlyList<IAttackActionView> folderChildren = folder.Children;
            if (folderChildren.Count == 0)
            {
                return;
            }

            IAttackActionView action = folderChildren[selectedActionIndex];
            switch (action)
            {
                case ISingleTargetAttackActionView singleTargetAction:
                    foreach (IAttackActionUi actionUi in actionUiFolder.Children)
                    {
                        if (actionUi is ISingleTargetAttackActionUi singleTargetActionUi)
                        {
                            singleTargetActionUi.View = singleTargetAction;
                            singleTargetActionUi.Show();

                            continue;
                        }

                        actionUi.Hide();
                    }
                break;
                case IAreaAttackActionView areaAction:
                    foreach (IAttackActionUi actionUi in actionUiFolder.Children)
                    {
                        if (actionUi is IAreaAttackActionUi areaActionUi)
                        {
                            areaActionUi.View = areaAction;
                            areaActionUi.Show();

                            continue;
                        }

                        actionUi.Hide();
                    }
                break;
                case IArcAngleAttackActionView arcAngleAction:
                    foreach (IAttackActionUi actionUi in actionUiFolder.Children)
                    {
                        if (actionUi is IArcAngleAttackActionUi arcAngleActionUi)
                        {
                            arcAngleActionUi.View = arcAngleAction;
                            arcAngleActionUi.Show();

                            continue;
                        }

                        actionUi.Hide();
                    }
                break;
                case IBurstAttackActionView burstAction:
                    foreach (IAttackActionUi actionUi in actionUiFolder.Children)
                    {
                        if (actionUi is IBurstAttackActionUi burstActionUi)
                        {
                            burstActionUi.View = burstAction;
                            burstActionUi.Show();

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
