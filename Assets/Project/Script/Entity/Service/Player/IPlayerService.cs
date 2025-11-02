namespace Mgfirefox.CrisisTd
{
    public interface IPlayerService : IService
    {
        IPlayerView Spawn();
        bool TrySpawn(out IPlayerView view);
    }
}
