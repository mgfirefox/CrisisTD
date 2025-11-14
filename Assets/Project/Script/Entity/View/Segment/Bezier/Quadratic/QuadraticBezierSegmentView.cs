namespace Mgfirefox.CrisisTd
{
    public class QuadraticBezierSegmentView : AbstractBezierSegmentView, IQuadraticBezierSegmentView
    {
        public override BezierType Type => BezierType.Quadratic;
    }
}
