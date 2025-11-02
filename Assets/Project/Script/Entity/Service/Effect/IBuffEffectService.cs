namespace Mgfirefox.CrisisTd
{
    public interface IBuffEffectService<in TISourceView> : IEffectService<BuffEffect, TISourceView>
        where TISourceView : class, IView
    {
    }
}
