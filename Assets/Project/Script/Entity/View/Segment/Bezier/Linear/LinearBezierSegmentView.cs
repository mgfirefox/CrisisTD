namespace Mgfirefox.CrisisTd
{
    public class LinearBezierSegmentView : AbstractBezierSegmentView, ILinearBezierSegmentView
    {
        public override BezierType Type => BezierType.Linear;

        public new int SegmentCount => 1;
    }
}
