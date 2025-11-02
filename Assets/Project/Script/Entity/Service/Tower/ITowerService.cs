using System.Collections.Generic;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface ITowerService : IService
    {
        IReadOnlyList<ITowerView> Towers { get; }
        int Count { get; }

        ITowerView Spawn(TowerId id, Vector3 position, Quaternion orientation);
        bool TrySpawn(TowerId id, Vector3 position, Quaternion orientation, out ITowerView view);
    }
}
