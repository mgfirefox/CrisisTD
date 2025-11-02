namespace Mgfirefox.CrisisTd
{
    public interface ICameraService : IService
    {
        ICameraView MainCamera { get; }

        bool IsVirtualCameraEnabled(IVirtualCameraView virtualCamera);

        void AddVirtualCamera(IVirtualCameraView virtualCamera, CameraPriority priority);
        void RemoveVirtualCamera(IVirtualCameraView virtualCamera);

        void EnableVirtualCamera(IVirtualCameraView virtualCamera);
        void DisableVirtualCamera(IVirtualCameraView virtualCamera);
    }
}
