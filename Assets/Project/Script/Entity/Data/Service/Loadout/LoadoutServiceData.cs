using System.Collections.Generic;

namespace Mgfirefox.CrisisTd
{
    public class LoadoutServiceData : AbstractServiceData
    {
        public IList<LoadoutItem> Loadout { get; set; } = new List<LoadoutItem>();
    }
}
