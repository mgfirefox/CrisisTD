namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractDebuffEffectService<TISourceView> :
        AbstractEffectService<DebuffEffect, TISourceView>, IDebuffEffectService<TISourceView>
        where TISourceView : class, IView
    {
    }
}
