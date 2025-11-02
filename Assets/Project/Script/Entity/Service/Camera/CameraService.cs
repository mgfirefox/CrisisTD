using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class CameraService : AbstractService, ICameraService
    {
        private readonly IDictionary<IVirtualCameraView, CameraPriority> priorities =
            new Dictionary<IVirtualCameraView, CameraPriority>();

        public ICameraView MainCamera { get; }

        [Inject]
        public CameraService(ICameraView mainCamera)
        {
            MainCamera = mainCamera;
        }

        public bool IsVirtualCameraEnabled(IVirtualCameraView virtualCamera)
        {
            if (virtualCamera == null)
            {
                throw new InvalidArgumentException(nameof(virtualCamera), null);
            }

            if (priorities.TryGetValue(virtualCamera, out CameraPriority priority))
            {
                return virtualCamera.Priority == (int)priority;
            }

            throw new InvalidArgumentException(nameof(virtualCamera), virtualCamera.ToString());
        }

        public void AddVirtualCamera(IVirtualCameraView virtualCamera, CameraPriority priority)
        {
            if (virtualCamera == null)
            {
                throw new InvalidArgumentException(nameof(virtualCamera), null);
            }

            if (priority <= CameraPriority.Disabled)
            {
                throw new InvalidArgumentException(nameof(priority), priority.ToString());
            }

            if (priorities.TryGetValue(virtualCamera, out CameraPriority oldPriority))
            {
                Debug.LogWarning(Warning.ObjectPropertyReplacedMessage(
                    typeof(IVirtualCameraView).ToString(), nameof(priority), oldPriority.ToString(),
                    priority.ToString()));
            }

            priorities[virtualCamera] = priority;
        }

        public void RemoveVirtualCamera(IVirtualCameraView virtualCamera)
        {
            if (virtualCamera is null)
            {
                throw new InvalidArgumentException(nameof(virtualCamera), null);
            }

            priorities.Remove(virtualCamera);
        }

        public void EnableVirtualCamera(IVirtualCameraView virtualCamera)
        {
            if (virtualCamera == null)
            {
                throw new InvalidArgumentException(nameof(virtualCamera), null);
            }

            if (priorities.TryGetValue(virtualCamera, out CameraPriority priority))
            {
                virtualCamera.Priority = (int)priority;

                return;
            }

            throw new InvalidArgumentException(nameof(virtualCamera), virtualCamera.ToString());
        }

        public void DisableVirtualCamera(IVirtualCameraView virtualCamera)
        {
            if (virtualCamera == null)
            {
                throw new InvalidArgumentException(nameof(virtualCamera), null);
            }

            virtualCamera.Priority = (int)CameraPriority.Disabled;
        }
    }
}
