using System;

namespace Mgfirefox.CrisisTd
{
    public interface ICooldownService : IDataService<CooldownServiceData>, ICooldownModel
    {
        event Action Finished;

        bool IsFinished { get; }

        void Update();

        void Reset();
        void Finish();
    }
}
