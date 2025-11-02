using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface ITowerFactory : IFactory
    {
        ITowerView Create(TowerId id, Vector3 position, Quaternion orientation);
        bool TryCreate(TowerId id, Vector3 position, Quaternion orientation, out ITowerView view);
    }
}
