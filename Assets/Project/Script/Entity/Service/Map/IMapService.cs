namespace Mgfirefox.CrisisTd
{
    public interface IMapService : IService, IMapModel
    {
        void Load(MapId id);
        bool TryLoad(MapId id);
        void Unload();
    }
}
