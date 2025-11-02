using System.Collections.Generic;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface IMapModel : IModel
    {
        MapId Id { get; }

        IReadOnlyList<Vector3> Waypoints { get; }
        Pose EnemySpawnPose { get; }
        Pose BasePose { get; }

        Pose PlayerSpawnPose { get; }

        bool IsLoaded { get; }
    }
}
