using System.Collections.Generic;

namespace Mgfirefox.CrisisTd
{
    public class LoadoutServiceData : AbstractServiceData
    {
        public IList<LoadoutItem> Items { get; set; } = new List<LoadoutItem>();
    }
}
