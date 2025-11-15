using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    [CreateAssetMenu(fileName = "TowerConfiguration", menuName = "Configuration/Tower")]
    public class TowerConfiguration : EntityConfiguration<TowerPresenter, TowerView,
        TowerLifetimeScope, TowerDataConfiguration>
    {
        [SerializeField]
        [BoxGroup("Tower")]
        private TowerModelDictionary modelPrefabs = new();
        
        public IDictionary<BranchLevel, ModelComponent> ModelPrefabs => modelPrefabs;
    }
}
