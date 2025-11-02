using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    [CreateAssetMenu(fileName = "EnemyConfiguration", menuName = "Configuration/Enemy")]
    public class EnemyConfiguration : EntityConfiguration<EnemyPresenter, EnemyView,
        EnemyLifetimeScope, EnemyDataConfiguration>
    {
    }
}
