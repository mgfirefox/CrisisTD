using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    [CreateAssetMenu(fileName = "EnemyConfiguration", menuName = "Configuration/Enemy")]
    public class EnemyConfiguration : EntityConfiguration<EnemyPresenter, EnemyView,
        EnemyLifetimeScope, EnemyDataConfiguration>
    {
        [SerializeField]
        [BoxGroup("Enemy")]
        private ModelComponent modelPrefab;
        
        public ModelComponent ModelPrefab => modelPrefab;
    }
}
