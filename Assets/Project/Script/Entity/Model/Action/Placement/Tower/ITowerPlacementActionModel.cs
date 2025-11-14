namespace Mgfirefox.CrisisTd
{
    public interface ITowerPlacementActionModel : IModel
    {
        int SelectedIndex { get; }

        int Limit { get; }
        int Count { get; }

        bool IsPlacing { get; }
        bool IsPlacementSuitable { get; }

        bool IsLimitReached { get; }
    }
}
