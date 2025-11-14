namespace Mgfirefox.CrisisTd
{
    public interface ISphereLogicalHitboxView : ILogicalHitboxView, ISphereHitboxModel
    {
        new float Radius { get; set; }
    }

    public interface ISphereLogicalHitboxView<TITargetView> : ISphereLogicalHitboxView,
        ILogicalHitboxView<TITargetView>
        where TITargetView : class, IView
    {
    }
}
