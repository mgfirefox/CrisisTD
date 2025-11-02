namespace Mgfirefox.CrisisTd
{
    public interface ITowerPlacementAction : IAction<TowerPlacementActionData>
    {
        int SelectedIndex { get; }

        int Count { get; }
        int Limit { get; }

        bool IsPlacing { get; }
        bool IsPlacementSuitable { get; }

        bool IsLimitReached { get; }

        void Select(int index);
        void Deselect();

        void Rotate();

        void UpdatePreview();
    }
}
