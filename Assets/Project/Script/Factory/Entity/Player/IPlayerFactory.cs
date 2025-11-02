using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface IPlayerFactory : IFactory
    {
        IPlayerView Create(Vector3 position, Quaternion orientation);
        bool TryCreate(Vector3 position, Quaternion orientation, out IPlayerView view);
    }
}
