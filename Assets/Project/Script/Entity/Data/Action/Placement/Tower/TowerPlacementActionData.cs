namespace Mgfirefox.CrisisTd
{
    public class TowerPlacementActionData : AbstractActionData
    {
        public int SelectedIndex { get; set; } = -1;

        public int Limit { get; set; }
        public int Count { get; set; }
    }
}
