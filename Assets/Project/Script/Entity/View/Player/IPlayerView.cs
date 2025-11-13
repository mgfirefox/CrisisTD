using System.Collections.Generic;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface IPlayerView : IView, IPlayerModel
    {
        IVirtualCameraView IsometricVirtualCamera { get; }
        IVirtualCameraView TopDownVirtualCamera { get; }

        IVirtualCameraFolder VirtualCameraFolder { get; }

        new Vector3 Position { get; set; }
        new Quaternion Orientation { get; set; }

        new float MaxMovementSpeed { get; set; }
        new float MovementSpeed { get; set; }

        new IReadOnlyList<LoadoutItem> Loadout { get; set; }
    }
}
