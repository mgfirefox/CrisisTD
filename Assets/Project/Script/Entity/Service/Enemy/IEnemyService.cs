using System.Collections.Generic;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface IEnemyService : IService
    {
        IReadOnlyList<IEnemyView> Enemies { get; }
        int Count { get; }

        IEnemyView Spawn(EnemyId id, Vector3 position, Quaternion orientation);
        IEnemyView Spawn(EnemyId id, Pose pose);
        bool TrySpawn(EnemyId id, Vector3 position, Quaternion orientation, out IEnemyView view);
        bool TrySpawn(EnemyId id, Pose pose, out IEnemyView view);

        void DespawnAll();
    }
}
