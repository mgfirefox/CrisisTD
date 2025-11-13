using System;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mgfirefox.CrisisTd
{
    public class TowerInteractionActionUi : AbstractActionUi, ITowerInteractionActionUi
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private TowerActionUiFolderView towerActionUiFolder;

        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private TextMeshProUGUI zeroBranchLevelText;
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private TextMeshProUGUI firstBranchLevelText;
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private TextMeshProUGUI secondBranchLevelText;

        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private Image singleBranchUpgradePanel;
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private Button singleBranchUpgradeButton;

        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private Image twoBranchUpgradePanel;
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private Button firstBranchUpgradeButton;
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private Button secondBranchUpgradeButton;

        private int selectedTowerActionIndex;

        public event Action SingleBranchUpgradeButtonClicked;

        public event Action FirstBranchUpgradeButtonClicked;
        public event Action SecondBranchUpgradeButtonClicked;

        public ITowerView InteractingTower
        {
            set
            {
                if (value == null)
                {
                    throw new InvalidArgumentException(nameof(value), null);
                }

                if (IsDestroyed)
                {
                    return;
                }

                SetTowerAction(value.ActionViewFolder);

                SetLevel(value.Level, value.MaxZeroBranchIndex);
                SetUpgradePanel(value);
            }
        }

        private void SetTowerAction(ITowerActionFolderView folder)
        {
            IReadOnlyList<ITowerActionView> folderChildren = folder.Children;
            if (folderChildren.Count == 0)
            {
                return;
            }

            ITowerActionView towerAction = folderChildren[selectedTowerActionIndex];
            switch (towerAction)
            {
                case IAttackTowerActionView attackTowerAction:
                    foreach (ITowerActionUi towerActionUi in towerActionUiFolder.Children)
                    {
                        if (towerActionUi is IAttackTowerActionUi attackTowerActionUi)
                        {
                            attackTowerActionUi.View = attackTowerAction;
                            attackTowerActionUi.Show();

                            continue;
                        }

                        towerActionUi.Hide();
                    }
                break;
                case ISupportTowerActionView supportTowerAction:
                    foreach (ITowerActionUi towerActionUi in towerActionUiFolder.Children)
                    {
                        if (towerActionUi is ISupportTowerActionUi supportTowerActionUi)
                        {
                            supportTowerActionUi.View = supportTowerAction;
                            supportTowerActionUi.Show();

                            continue;
                        }

                        towerActionUi.Hide();
                    }
                break;
                default:
                    // TODO: Change Exception
                    throw new InvalidArgumentException(nameof(folder), folder.ToString());
            }
        }

        private void SetLevel(BranchLevel level, int maxZeroBranchLevelIndex)
        {
            if (!BranchLevelValidator.TryValidate(level))
            {
                throw new InvalidArgumentException(nameof(level), level.ToString());
            }

            switch (level.Type)
            {
                case BranchType.Zero:
                    zeroBranchLevelText.text = level.Index.ToString();
                    firstBranchLevelText.text = "X";
                    secondBranchLevelText.text = "X";
                break;
                case BranchType.First:
                    zeroBranchLevelText.text = maxZeroBranchLevelIndex.ToString();
                    firstBranchLevelText.text = level.Index.ToString();
                    secondBranchLevelText.text = "X";
                break;
                case BranchType.Second:
                    zeroBranchLevelText.text = maxZeroBranchLevelIndex.ToString();
                    firstBranchLevelText.text = "X";
                    secondBranchLevelText.text = level.Index.ToString();
                break;
                case BranchType.Undefined:
                default:
                    throw new InvalidArgumentException(nameof(level), level.ToString());
            }
        }

        private void SetUpgradePanel(ITowerView tower)
        {
            if (!BranchLevelValidator.TryValidate(tower.Level))
            {
                throw new InvalidArgumentException(nameof(tower), tower.ToString());
            }

            switch (tower.Level.Type)
            {
                case BranchType.Zero:
                    if (tower.Level.Index == tower.MaxZeroBranchIndex)
                    {
                        singleBranchUpgradePanel.gameObject.SetActive(false);
                        twoBranchUpgradePanel.gameObject.SetActive(true);

                        break;
                    }

                    singleBranchUpgradePanel.gameObject.SetActive(true);
                    twoBranchUpgradePanel.gameObject.SetActive(false);
                break;
                case BranchType.First:
                case BranchType.Second:
                    singleBranchUpgradePanel.gameObject.SetActive(true);
                    twoBranchUpgradePanel.gameObject.SetActive(false);
                break;
                case BranchType.Undefined:
                default:
                    throw new InvalidArgumentException(nameof(tower), tower.ToString());
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            towerActionUiFolder.Initialize();

            singleBranchUpgradeButton.onClick.AddListener(OnSingleBranchUpgradeButtonClicked);

            firstBranchUpgradeButton.onClick.AddListener(OnFirstBranchUpgradeButtonClicked);
            secondBranchUpgradeButton.onClick.AddListener(OnSecondBranchUpgradeButtonClicked);
        }

        protected override void OnDestroying()
        {
            singleBranchUpgradeButton.onClick.RemoveListener(OnSingleBranchUpgradeButtonClicked);

            firstBranchUpgradeButton.onClick.RemoveListener(OnFirstBranchUpgradeButtonClicked);
            secondBranchUpgradeButton.onClick.RemoveListener(OnSecondBranchUpgradeButtonClicked);

            towerActionUiFolder.Destroy();

            base.OnDestroying();
        }

        private void OnSingleBranchUpgradeButtonClicked()
        {
            SingleBranchUpgradeButtonClicked?.Invoke();
        }

        private void OnFirstBranchUpgradeButtonClicked()
        {
            FirstBranchUpgradeButtonClicked?.Invoke();
        }

        private void OnSecondBranchUpgradeButtonClicked()
        {
            SecondBranchUpgradeButtonClicked?.Invoke();
        }
    }
}
