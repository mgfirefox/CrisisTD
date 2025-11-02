namespace Mgfirefox.CrisisTd
{
    public interface IBaseFactory : IFactory
    {
        IBaseView Create();
        bool TryCreate(out IBaseView view);
    }
}
