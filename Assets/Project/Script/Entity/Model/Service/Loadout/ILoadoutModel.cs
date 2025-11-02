using System.Collections.Generic;

namespace Mgfirefox.CrisisTd
{
    public interface ILoadoutModel
    {
        IReadOnlyList<LoadoutItem> Loadout { get; }
    }
}
