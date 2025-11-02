namespace Mgfirefox.CrisisTd
{
    public interface ITowerPlacementActionView : IActionView, ITowerPlacementActionModel
    {
        new int SelectedIndex { get; set; }

        new int Limit { get; set; }
        new int Count { get; set; }

        new bool IsPlacing { get; set; }
        new bool IsPlacementSuitable { get; set; }

        new bool IsLimitReached { get; set; }
    }
}
