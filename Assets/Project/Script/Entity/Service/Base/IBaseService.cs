using System.Collections.Generic;

namespace Mgfirefox.CrisisTd
{
    public interface IBaseService : IService
    {
        IReadOnlyCollection<IBaseView> Bases { get; }
        int Count { get; }

        IBaseView Spawn();
        bool TrySpawn(out IBaseView view);

        IBaseView Get(int index);

        void DespawnAll();
    }
}
