namespace Mgfirefox.CrisisTd
{
    public interface
        IDebuffEffectService<in TISourceView> : IEffectService<DebuffEffect, TISourceView>
        where TISourceView : class, IView
    {
    }
}
