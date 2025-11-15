using System.Collections.Generic;

namespace Mgfirefox.CrisisTd
{
    public class TowerModelServiceData : AbstractServiceData
    {
        public IDictionary<BranchLevel, IModelComponent> Models { get; set; } = new Dictionary<BranchLevel, IModelComponent>();
    }
}
