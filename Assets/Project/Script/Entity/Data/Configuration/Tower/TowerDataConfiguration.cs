using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    [CreateAssetMenu(fileName = "TowerDataConfiguration", menuName = "DataConfiguration/Tower")]
    public class TowerDataConfiguration : ScriptableObject
    {
        [SerializeField]
        [BoxGroup("Tower")]
        [Dropdown("validTypes")]
        private TowerType type = TowerType.Undefined;
        private TowerType[] validTypes = TowerTypeValidator.ValidTypes.ToArray();

        [SerializeField]
        [BoxGroup("Tower")]
        private LevelDataConfigurationDictionary levelDataConfigurations = new();
        
        [SerializeField]
        [BoxGroup("Tower")]
        [Dropdown("validPriorities")]
        private TargetPriority priority = TargetPriority.First;
        private TargetPriority[] validPriorities =
            TargetPriorityValidator.ValidPriorities.ToArray();

        public TowerType Type => type;

        public IDictionary<BranchLevel, LevelDataConfiguration> LevelDataConfigurations =>
            levelDataConfigurations;

        public TargetPriority Priority => priority;
    }
}
