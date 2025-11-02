namespace Mgfirefox.CrisisTd
{
    public interface ITowerInteractionAction : IAction<TowerInteractionActionData>
    {
        bool IsInteracting { get; }

        void UpgradeBranch1();
        void UpgradeBranch2();

        void Sell();

        void Cancel();
    }
}
