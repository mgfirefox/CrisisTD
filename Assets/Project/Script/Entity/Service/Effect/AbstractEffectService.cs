using System;
using System.Collections.Generic;

namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractEffectService<TEffect, TISourceView> : AbstractService,
        IEffectService<TEffect, TISourceView>
        where TEffect : Effect, new()
        where TISourceView : class, IView
    {
        private readonly IDictionary<EffectType, SourceGroupEffectItem<TEffect, TISourceView>>
            items = new Dictionary<EffectType, SourceGroupEffectItem<TEffect, TISourceView>>();
        private readonly IDictionary<TISourceView, Action> sourceDestroyingActions =
            new Dictionary<TISourceView, Action>();

        protected virtual bool BelongsToSameGroup(TISourceView source, TISourceView groupSource,
            float epsilon)
        {
            return true;
        }

        protected virtual TEffect GroupEffectSelector(TEffect effect, TEffect groupEffect,
            float epsilon)
        {
            return effect.Value.IsGreaterThanApproximately(groupEffect.Value, epsilon)
                ? effect
                : groupEffect;
        }

        private TEffect GetGroupEffect(EffectType type, IList<TEffect> effects, float epsilon)
        {
            var groupEffect = new TEffect
            {
                Type = type,
            };

            foreach (TEffect effect in effects)
            {
                groupEffect = GroupEffectSelector(effect, groupEffect, epsilon);
            }

            return groupEffect;
        }

        protected virtual TEffect GetEffect(EffectType type, IList<TEffect> groupEffects,
            float epsilon)
        {
            var effect = new TEffect
            {
                Type = type,
            };

            foreach (TEffect groupEffect in groupEffects)
            {
                effect.Value += groupEffect.Value;
            }

            return effect;
        }

        public event Action<TEffect> Changed;

        public void Apply(TEffect effect, TISourceView source, float epsilon)
        {
            if (!EffectValidator.TryValidate(effect, epsilon))
            {
                // TODO: Change Exception
                throw new InvalidArgumentException(nameof(effect), effect.ToString());
            }

            if (source == null)
            {
                throw new InvalidArgumentException(nameof(source), null);
            }

            if (!sourceDestroyingActions.ContainsKey(source))
            {
                Action sourceDestroyingAction = () => RemoveSource(source, epsilon);

                source.Destroying += sourceDestroyingAction;

                sourceDestroyingActions[source] = sourceDestroyingAction;
            }

            if (items.TryGetValue(effect.Type,
                    out SourceGroupEffectItem<TEffect, TISourceView> item))
            {
            }
            else
            {
                item = new SourceGroupEffectItem<TEffect, TISourceView>(effect.Type,
                    BelongsToSameGroup, GetGroupEffect, GetEffect, epsilon);

                items[effect.Type] = item;
            }

            TEffect oldGroupEffect = item.GetGroupEffect(source);

            item.UpdateSourceEffect(source, effect.Clone() as TEffect);

            TEffect newGroupEffect = item.GetGroupEffect(source);

            TEffect selectedGroupEffect =
                GroupEffectSelector(newGroupEffect, oldGroupEffect, epsilon);

            if (selectedGroupEffect.Value.EqualsApproximately(oldGroupEffect.Value, epsilon))
            {
                return;
            }

            TEffect newEffect = item.GetEffect();

            Changed?.Invoke(newEffect.Clone() as TEffect);
        }

        public void RemoveSource(TISourceView source, float epsilon)
        {
            if (source is null)
            {
                throw new InvalidArgumentException(nameof(source), null);
            }

            source.Destroying -= sourceDestroyingActions[source];

            foreach ((EffectType _, SourceGroupEffectItem<TEffect, TISourceView> item) in items)
            {
                if (item.ContainsSource(source))
                {
                    TEffect oldGroupEffect = item.GetGroupEffect(source);

                    item.RemoveSource(source);

                    TEffect newGroupEffect = item.GetGroupEffect(source);

                    TEffect selectedGroupEffect =
                        GroupEffectSelector(newGroupEffect, oldGroupEffect, epsilon);

                    if (selectedGroupEffect.Value.EqualsApproximately(newGroupEffect.Value,
                            epsilon))
                    {
                        return;
                    }

                    TEffect newEffect = item.GetEffect();

                    Changed?.Invoke(newEffect.Clone() as TEffect);
                }
            }
            sourceDestroyingActions.Remove(source);
        }

        public TEffect Get(EffectType type)
        {
            if (!EffectTypeValidator.TryValidate(type))
            {
                throw new InvalidArgumentException(nameof(type), type.ToString());
            }

            if (items.TryGetValue(type, out SourceGroupEffectItem<TEffect, TISourceView> item))
            {
                return item.GetEffect().Clone() as TEffect;
            }

            return new TEffect
            {
                Type = type,
            };
        }

        public void Clear()
        {
            foreach ((TISourceView source, Action action) in sourceDestroyingActions)
            {
                source.Destroying -= action;
            }
            sourceDestroyingActions.Clear();

            items.Clear();
        }
    }
}
