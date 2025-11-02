namespace Mgfirefox.CrisisTd
{
    public interface IMovingTransformService<in TData> : ITransformService<TData>,
        IMovingTransformModel
        where TData : MovingTransformServiceData
    {
    }
}
