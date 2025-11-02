using System.Collections.Generic;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface IEnemyService : IService
    {
        IReadOnlyList<IEnemyView> Enemies { get; }
        int Count { get; }

        IEnemyView Spawn(Vector3 position, Quaternion orientation);
        IEnemyView Spawn(Pose pose);
        bool TrySpawn(Vector3 position, Quaternion orientation, out IEnemyView view);
        bool TrySpawn(Pose pose, out IEnemyView view);

        void DespawnAll();
    }
}
