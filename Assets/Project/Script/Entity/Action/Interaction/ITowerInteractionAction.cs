namespace Mgfirefox.CrisisTd
{
    public interface ITowerInteractionAction : IAction<TowerInteractionActionData>
    {
        bool IsInteracting { get; }

        void UpgradeFirstBranch();
        void UpgradeSecondBranch();

        void Sell();

        void Cancel();
    }
}
