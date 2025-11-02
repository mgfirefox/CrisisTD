using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    [CreateAssetMenu(fileName = "TowerConfiguration", menuName = "Configuration/Tower")]
    public class TowerConfiguration : EntityConfiguration<TowerPresenter, TowerView,
        TowerLifetimeScope, TowerDataConfiguration>
    {
    }
}
