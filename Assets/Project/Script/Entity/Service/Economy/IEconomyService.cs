namespace Mgfirefox.CrisisTd
{
    public interface IEconomyService : IService, IEconomyModel
    {
        void Initialize(float money);
        
        bool TryPlaceTower(TowerId id, float epsilon);
        bool TryUpgradeTower(ITowerView tower, NextBranchLevel nextLevel, float epsilon);
        void SellTower(ITowerView tower);
        void KillEnemy(IEnemyView enemy);
    }
}
