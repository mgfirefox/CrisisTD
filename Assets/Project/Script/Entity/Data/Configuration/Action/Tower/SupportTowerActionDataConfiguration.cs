using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    [CreateAssetMenu(fileName = "TowerActionDataConfiguration",
        menuName = "DataConfiguration/Action/Tower/Support")]
    public class SupportTowerActionDataConfiguration : AbstractTowerActionDataConfiguration
    {
        [SerializeField]
        [BoxGroup("Tower")]
        private List<AbstractBuffActionDataConfiguration> actionDataConfigurations = new();

        public IList<AbstractBuffActionDataConfiguration> ActionDataConfigurations =>
            actionDataConfigurations.ToList();
    }
}
