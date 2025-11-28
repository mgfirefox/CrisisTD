using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    [CreateAssetMenu(fileName = "LevelDataConfiguration", menuName = "DataConfiguration/Level")]
    public class LevelDataConfiguration : ScriptableObject
    {
        [SerializeField]
        [BoxGroup("Level")]
        private List<AbstractTowerActionDataConfiguration> towerActionDataConfigurations = new();

        [SerializeField]
        [BoxGroup("Level")]
        private float upgradeCost;

        public IList<AbstractTowerActionDataConfiguration> TowerActionDataConfigurations =>
            towerActionDataConfigurations.ToList();
        
        public float UpgradeCost => upgradeCost;
    }
}
