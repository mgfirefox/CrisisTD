using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class PlayerView : AbstractView, IPlayerView
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private VirtualCameraFolderView virtualCameraFolder;
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private VirtualCameraView isometricVirtualCamera;
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private VirtualCameraView topDownVirtualCamera;

        [SerializeField]
        [BoxGroup("Transform")]
        [ReadOnly]
        private Vector3 position;
        [SerializeField]
        [BoxGroup("Transform")]
        [ReadOnly]
        private Quaternion orientation;

        [SerializeField]
        [BoxGroup("Transform")]
        [ReadOnly]
        private float maxMovementSpeed;
        [SerializeField]
        [BoxGroup("Transform")]
        [ReadOnly]
        private float movementSpeed;

        [SerializeField]
        [BoxGroup("Loadout")]
        [ReadOnly]
        private List<LoadoutItem> loadout;

        public IVirtualCameraView IsometricVirtualCamera => isometricVirtualCamera;
        public IVirtualCameraView TopDownVirtualCamera => topDownVirtualCamera;

        public IVirtualCameraFolderView VirtualCameraFolder => virtualCameraFolder;

        public Vector3 Position
        {
            get => position;
            set
            {
                position = value;

                if (IsDestroyed)
                {
                    return;
                }

                Transform.position = position;
            }
        }
        public Quaternion Orientation
        {
            get => orientation;
            set
            {
                orientation = value;

                if (IsDestroyed)
                {
                    return;
                }

                Transform.rotation = orientation;
            }
        }

        public float MaxMovementSpeed { get => maxMovementSpeed; set => maxMovementSpeed = value; }
        public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }

        public IReadOnlyList<LoadoutItem> Loadout
        {
            get => loadout.AsReadOnly();
            set => loadout = value.ToList();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            virtualCameraFolder.Initialize();
        }

        protected override void OnDestroying()
        {
            virtualCameraFolder.Destroy();

            base.OnDestroying();
        }
    }
}
