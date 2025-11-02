namespace Mgfirefox.CrisisTd
{
    public interface ITowerAction : IAction
    {
        void ShowInteraction();
        void HideInteraction();
    }

    public interface ITowerAction<in TData> : IAction<TData>, ITowerAction
        where TData : AbstractTowerActionData
    {
    }
}
