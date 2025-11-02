namespace Mgfirefox.CrisisTd
{
    public interface IDataService : ISceneObject, IService
    {
    }

    public interface IDataService<in TData> : ISceneObject<TData>, IDataService
        where TData : AbstractServiceData
    {
    }
}
