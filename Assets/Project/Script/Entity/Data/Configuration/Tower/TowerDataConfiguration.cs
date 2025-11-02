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
        /*[SerializeField]
        [BoxGroup("Tower")]
        [ShowIf("IsAttackType")]
        private List<AttackTowerActionDataConfiguration> attackActionDataConfigurations = new();
        private bool IsAttackType => type == TowerType.Attack;
        [SerializeField]
        [BoxGroup("Tower")]
        [ShowIf("IsSupportType")]
        private List<SupportTowerActionDataConfiguration> supportActionDataConfigurations = new();
        private bool IsSupportType => type == TowerType.Support;*/

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
        /*public IList<AbstractTowerActionDataConfiguration> ActionDataConfigurations
        {
            get
            {
                switch (Type)
                {
                    case TowerType.Attack:
                        IList<AbstractTowerActionDataConfiguration>
                            abstractActionDataConfigurations = attackActionDataConfigurations
                                .Cast<AbstractTowerActionDataConfiguration,
                                    AttackTowerActionDataConfiguration>();

                        return abstractActionDataConfigurations.ToList();
                    case TowerType.Support:
                        abstractActionDataConfigurations = supportActionDataConfigurations
                            .Cast<AbstractTowerActionDataConfiguration,
                                SupportTowerActionDataConfiguration>();

                        return abstractActionDataConfigurations.ToList();
                    case TowerType.Undefined:
                    default:
                        // TODO: Change Warning
                        Debug.LogWarning($"Type {Type} is not valid. Empty list will be returned.",
                            this);

                        return new List<AbstractTowerActionDataConfiguration>();
                }
            }
        }*/
        public IDictionary<LevelIndex, LevelDataConfiguration> LevelDataConfigurations =>
            levelDataConfigurations;

        public TargetPriority Priority => priority;
    }
}
