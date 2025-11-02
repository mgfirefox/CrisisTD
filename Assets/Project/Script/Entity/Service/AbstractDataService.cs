namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractDataService<TData> : AbstractSceneObject<TData>,
        IDataService<TData>
        where TData : AbstractServiceData
    {
        protected AbstractDataService(Scene scene) : base(scene)
        {
        }
    }
}
