using System;

namespace Mgfirefox.CrisisTd
{
    public interface IAllEffectService : IDataService
    {
        event Action<Effect> Changed;

        event Action<Effect> RangeChanged;

        void Reapply();
    }

    public interface IAllEffectService<in TData, in TISourceView> : IAllEffectService,
        IDataService<TData>, IAllEffectModel
        where TData : AbstractAllEffectServiceData
        where TISourceView : class, IView
    {
        void Apply(Effect effect, TISourceView source);
    }
}
