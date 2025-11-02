using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface IEnemyFactory : IFactory
    {
        IEnemyView Create(Vector3 position, Quaternion orientation);
        bool TryCreate(Vector3 position, Quaternion orientation, out IEnemyView view);
    }
}
