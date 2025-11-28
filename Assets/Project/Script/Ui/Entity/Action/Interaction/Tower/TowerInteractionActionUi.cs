using System;
using System.Collections.Generic;
using System.Globalization;
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
        private TowerActionUiFolder towerActionUiFolder;

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
        private TextMeshProUGUI singleBranchUpgradeButtonText;

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
        private TextMeshProUGUI firstBranchUpgradeButtonText;
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private Button secondBranchUpgradeButton;
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private TextMeshProUGUI secondBranchUpgradeButtonText;
        
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private Button sellButton;
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private TextMeshProUGUI sellButtonText;
        
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private TextMeshProUGUI totalCostText;
        
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private TextMeshProUGUI priorityText;



        private int selectedTowerActionIndex;

        public event Action SingleBranchUpgradeButtonClicked;

        public event Action FirstBranchUpgradeButtonClicked;
        public event Action SecondBranchUpgradeButtonClicked;
        
        public event Action SellButtonClicked;

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

                SetTowerAction(value.ActionFolder);

                SetLevel(value.Level, value.MaxZeroBranchIndex);
                SetUpgradePanel(value);

                SetDataPanel(value);
            }
        }

        private void SetTowerAction(ITowerActionFolder folder)
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
            
            IList<NextBranchLevel> nextLevels = tower.NextLevels;
            switch (nextLevels.Count)
            {
                case 0:
                    singleBranchUpgradePanel.gameObject.SetActive(false);
                    twoBranchUpgradePanel.gameObject.SetActive(false);
                break;
                case 1:
                    singleBranchUpgradePanel.gameObject.SetActive(true);
                    twoBranchUpgradePanel.gameObject.SetActive(false);

                    singleBranchUpgradeButtonText.text = nextLevels[0].UpgradeCost.ToString(CultureInfo.InvariantCulture);
                break;
                default:
                    singleBranchUpgradePanel.gameObject.SetActive(false);
                    twoBranchUpgradePanel.gameObject.SetActive(true);
                    
                    firstBranchUpgradeButtonText.text = nextLevels[0].UpgradeCost.ToString(CultureInfo.InvariantCulture);
                    secondBranchUpgradeButtonText.text = nextLevels[1].UpgradeCost.ToString(CultureInfo.InvariantCulture);
                break;
            }
        }

        private void SetDataPanel(ITowerView tower)
        {
            sellButtonText.text = Mathf.FloorToInt(tower.TotalCost / Constant.towerSellingRatioDenominator).ToString();
            
            totalCostText.text = Mathf.FloorToInt(tower.TotalCost).ToString();
            
            priorityText.text = tower.Priority.ToString();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            towerActionUiFolder.Initialize();

            singleBranchUpgradeButton.onClick.AddListener(OnSingleBranchUpgradeButtonClicked);

            firstBranchUpgradeButton.onClick.AddListener(OnFirstBranchUpgradeButtonClicked);
            secondBranchUpgradeButton.onClick.AddListener(OnSecondBranchUpgradeButtonClicked);
            
            sellButton.onClick.AddListener(OnSellButtonClicked);
        }

        protected override void OnDestroying()
        {
            singleBranchUpgradeButton.onClick.RemoveListener(OnSingleBranchUpgradeButtonClicked);

            firstBranchUpgradeButton.onClick.RemoveListener(OnFirstBranchUpgradeButtonClicked);
            secondBranchUpgradeButton.onClick.RemoveListener(OnSecondBranchUpgradeButtonClicked);
            
            sellButton.onClick.RemoveListener(OnSellButtonClicked);

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

        private void OnSellButtonClicked()
        {
            SellButtonClicked?.Invoke();
        }
    }
}
