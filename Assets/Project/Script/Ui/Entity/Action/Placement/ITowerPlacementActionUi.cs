using System;

namespace Mgfirefox.CrisisTd
{
    public interface ITowerPlacementActionUi : IActionUi
    {
        int Count { set; }
        int Limit { set; }

        event Action<int> TowerButtonClicked;
    }
}
