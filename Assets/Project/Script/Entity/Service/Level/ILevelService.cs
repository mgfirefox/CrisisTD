using System;
using Mgfirefox.CrisisTd.Level;

namespace Mgfirefox.CrisisTd
{
    public interface ILevelService : IDataService<LevelServiceData>, ILevelModel
    {
        event Action<LevelItem> Changed;

        void UpgradeFirstBranch();
        void UpgradeSecondBranch();
    }
}
