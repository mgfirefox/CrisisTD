using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface IEnemyFactory : IFactory
    {
        IEnemyView Create(EnemyId id, Vector3 position, Quaternion orientation);
        bool TryCreate(EnemyId id, Vector3 position, Quaternion orientation, out IEnemyView view);
    }
}
