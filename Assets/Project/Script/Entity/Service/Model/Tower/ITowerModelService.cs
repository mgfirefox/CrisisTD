using System;

namespace Mgfirefox.CrisisTd
{
    public interface ITowerModelService : IDataService<TowerModelServiceData>
    {
        event Action<IModelComponent> Changed;
    }
}
