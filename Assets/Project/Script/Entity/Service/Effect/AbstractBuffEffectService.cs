namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractBuffEffectService<TISourceView> :
        AbstractEffectService<BuffEffect, TISourceView>, IBuffEffectService<TISourceView>
        where TISourceView : class, IView
    {
    }
}
