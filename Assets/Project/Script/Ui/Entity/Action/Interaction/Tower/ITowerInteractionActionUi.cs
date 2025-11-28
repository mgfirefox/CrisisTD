using System;

namespace Mgfirefox.CrisisTd
{
    public interface ITowerInteractionActionUi : IActionUi
    {
        ITowerView InteractingTower { set; }

        event Action SingleBranchUpgradeButtonClicked;
        event Action FirstBranchUpgradeButtonClicked;
        event Action SecondBranchUpgradeButtonClicked;
        
        event Action SellButtonClicked;
    }
}
