using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    [CreateAssetMenu(fileName = "TowerActionDataConfiguration",
        menuName = "DataConfiguration/Action/Tower/Attack")]
    public class AttackTowerActionDataConfiguration : AbstractTowerActionDataConfiguration
    {
        [SerializeField]
        [BoxGroup("Tower")]
        private List<AbstractAttackActionDataConfiguration> actionDataConfigurations = new();

        public IList<AbstractAttackActionDataConfiguration> ActionDataConfigurations =>
            actionDataConfigurations.ToList();
    }
}
