using System;

namespace Mgfirefox.CrisisTd
{
    public interface IEffectService : IService
    {
        void Clear();
    }

    public interface IEffectService<TEffect, in TISourceView> : IEffectService
        where TEffect : Effect, new()
        where TISourceView : class, IView
    {
        event Action<TEffect> Changed;

        void Apply(TEffect effect, TISourceView source, float epsilon);

        void RemoveSource(TISourceView source, float epsilon);

        TEffect Get(EffectType type);
    }
}
