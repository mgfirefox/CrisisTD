using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class EconomyService : AbstractService, IEconomyService
    {
        private readonly IEconomyServiceUi ui;
        
        private readonly IDictionary<TowerId, float> towerPlacementCosts = new Dictionary<TowerId, float>();
        
        public float Money { get; private set; }
        
        [Inject]
        public EconomyService(IEconomyServiceUi ui, IReadOnlyDictionary<TowerId, TowerConfiguration> towerConfigurations)
        {
            this.ui = ui;
            InitializeTowerPlacementCosts(towerConfigurations);
        }
        
        private void InitializeTowerPlacementCosts(IReadOnlyDictionary<TowerId, TowerConfiguration> towerConfigurations)
        {
            foreach ((TowerId id, TowerConfiguration configuration) in towerConfigurations)
            {
                IDictionary<BranchLevel, LevelDataConfiguration> levelDataConfigurations = configuration.DataConfiguration.LevelDataConfigurations;
                if (levelDataConfigurations.TryGetValue(new BranchLevel(),
                        out LevelDataConfiguration levelDataConfiguration))
                {
                    towerPlacementCosts[id] = levelDataConfiguration.UpgradeCost;
                }
                else
                {
                    towerPlacementCosts[id] = 0.0f;
                }
            }
        }

        public void Initialize(float money)
        {
            Money = money;
            
            ui.Money = Mathf.FloorToInt(Money);
        }

        public bool TryPlaceTower(TowerId id, float epsilon)
        {
            if (!TowerIdValidator.TryValidate(id))
            {
                throw new InvalidArgumentException(nameof(id), id.ToString());
            }

            float cost = towerPlacementCosts[id];
            if (TryPayAction(cost, epsilon))
            {
                return true;
            }

            return false;
        }

        public bool TryUpgradeTower(ITowerView tower, NextBranchLevel nextLevel, float epsilon)
        {
            if (TryPayAction(nextLevel.UpgradeCost, epsilon))
            {
                return true;
            }

            return false;
        }

        public void SellTower(ITowerView tower)
        {
            Money += tower.TotalCost / Constant.towerSellingRatioDenominator;
            
            ui.Money = Mathf.FloorToInt(Money);
        }

        public void KillEnemy(IEnemyView enemy)
        {
            Money += enemy.MaxHealth;
            
            ui.Money = Mathf.FloorToInt(Money);
        }

        private bool TryPayAction(float cost, float epsilon)
        {
            if (Money.IsLessThanApproximately(cost, epsilon))
            {
                return false;
            }

            Money -= cost;
            if (Money.IsApproximateZero(epsilon))
            {
                Money = 0.0f;
            }
            
            ui.Money = Mathf.FloorToInt(Money);
            
            return true;
        }
    }
}
