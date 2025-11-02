namespace Mgfirefox.CrisisTd
{
    public interface ITowerActionDataFactory : IDataFactory
    {
        AbstractTowerActionData Create(TowerType type,
            AbstractTowerActionDataConfiguration configuration, TargetPriority targetPriority);

        bool TryCreate(TowerType type, AbstractTowerActionDataConfiguration configuration,
            TargetPriority targetPriority, out AbstractTowerActionData data);
    }
}
