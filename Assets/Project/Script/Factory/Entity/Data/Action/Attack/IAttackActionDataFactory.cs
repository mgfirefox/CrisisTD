namespace Mgfirefox.CrisisTd
{
    public interface IAttackActionDataFactory : IDataFactory
    {
        AbstractAttackActionData Create(AbstractAttackActionDataConfiguration configuration);

        bool TryCreate(AbstractAttackActionDataConfiguration configuration,
            out AbstractAttackActionData data);
    }
}
