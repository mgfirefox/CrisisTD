namespace Mgfirefox.CrisisTd
{
    public interface IBuffActionDataFactory : IDataFactory
    {
        AbstractBuffActionData Create(AbstractBuffActionDataConfiguration configuration);

        bool TryCreate(AbstractBuffActionDataConfiguration configuration,
            out AbstractBuffActionData data);
    }
}
