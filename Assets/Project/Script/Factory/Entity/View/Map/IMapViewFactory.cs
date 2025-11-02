namespace Mgfirefox.CrisisTd
{
    public interface IMapViewFactory : IFactory
    {
        IMapView Create(MapId id);
        bool TryCreate(MapId id, out IMapView view);
    }
}
