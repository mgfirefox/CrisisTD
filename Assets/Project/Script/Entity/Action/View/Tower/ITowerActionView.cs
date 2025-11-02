namespace Mgfirefox.CrisisTd
{
    public interface ITowerActionView : IActionView, ITowerActionModel
    {
        new float RangeRadius { get; set; }

        new TargetPriority TargetPriority { get; set; }
    }
}
