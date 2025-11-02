namespace Mgfirefox.CrisisTd
{
    public interface ITransformService<in TData> : IDataService<TData>, ITransformModel
        where TData : TransformServiceData
    {
    }
}
