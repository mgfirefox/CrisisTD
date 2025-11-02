using System;
using System.Collections.Generic;
using System.Linq;

namespace Mgfirefox.CrisisTd
{
    public class SourceGroupEffectItem<TEffect, TISourceView>
        where TEffect : Effect, new()
        where TISourceView : class, IView
    {
        private readonly EffectType type;

        private readonly IDictionary<TISourceView, ISet<TISourceView>> groups =
            new Dictionary<TISourceView, ISet<TISourceView>>();
        private readonly IDictionary<TISourceView, TEffect> effects =
            new Dictionary<TISourceView, TEffect>();

        private readonly Func<TISourceView, TISourceView, float, bool> belongsToSameGroupFunction;
        private readonly Func<EffectType, IList<TEffect>, float, TEffect> getGroupEffectFunction;
        private readonly Func<EffectType, IList<TEffect>, float, TEffect> getEffectFunction;

        private readonly float epsilon;

        public SourceGroupEffectItem(EffectType type,
            Func<TISourceView, TISourceView, float, bool> belongsToSameGroupFunction,
            Func<EffectType, IList<TEffect>, float, TEffect> getGroupEffectFunction,
            Func<EffectType, IList<TEffect>, float, TEffect> getEffectFunction, float epsilon)
        {
            this.type = type;
            this.belongsToSameGroupFunction = belongsToSameGroupFunction;
            this.getGroupEffectFunction = getGroupEffectFunction;
            this.getEffectFunction = getEffectFunction;
            this.epsilon = epsilon;
        }

        public void UpdateSourceEffect(TISourceView source, TEffect effect)
        {
            if (source == null)
            {
                throw new InvalidArgumentException(nameof(source), null);
            }

            if (!EffectValidator.TryValidate(effect, epsilon))
            {
                // TODO: Change Exception
                throw new InvalidArgumentException(nameof(effect), effect.ToString());
            }

            TISourceView groupSource = GetGroup(source);
            if (groupSource == null)
            {
                var group = new HashSet<TISourceView>
                {
                    source,
                };

                groups[source] = group;
            }
            else if (source != groupSource)
            {
                groups[groupSource].Add(source);
            }

            effects[source] = effect;
        }

        public TEffect GetGroupEffect(TISourceView source)
        {
            if (source is null)
            {
                throw new InvalidArgumentException(nameof(source), null);
            }

            TISourceView groupSource = GetGroup(source);
            if (groupSource == null)
            {
                return new TEffect
                {
                    Type = type,
                };
            }

            IList<TEffect> effects = new List<TEffect>();

            ISet<TISourceView> group = groups[groupSource];
            foreach (TISourceView source1 in group)
            {
                if (this.effects.TryGetValue(source1, out TEffect effect))
                {
                    effects.Add(effect);
                }
            }

            TEffect groupEffect = getGroupEffectFunction(type, effects, epsilon);
            if (!EffectValidator.TryValidate(groupEffect, epsilon))
            {
                // TODO: Change Exception
                throw new InvalidArgumentException(nameof(groupEffect), groupEffect.ToString());
            }

            return groupEffect;
        }

        public TEffect GetEffect()
        {
            IList<TEffect> effects = new List<TEffect>();

            foreach (TISourceView groupSource in groups.Keys)
            {
                TEffect effect = GetGroupEffect(groupSource);

                effects.Add(effect);
            }

            TEffect finalEffect = getEffectFunction(type, effects, epsilon);
            if (!EffectValidator.TryValidate(finalEffect, epsilon))
            {
                // TODO: Change Exception
                throw new InvalidArgumentException(nameof(finalEffect), finalEffect.ToString());
            }

            return finalEffect;
        }

        public bool RemoveSource(TISourceView source)
        {
            if (source is null)
            {
                throw new InvalidArgumentException(nameof(source), null);
            }

            if (!effects.Remove(source))
            {
                return false;
            }

            TISourceView groupSource = GetGroup(source);
            if (groupSource == null)
            {
                return false;
            }

            ISet<TISourceView> group = groups[groupSource];
            group.Remove(source);

            if (source != groupSource)
            {
                return true;
            }

            groups.Remove(groupSource);

            if (group.Count == 0)
            {
                return true;
            }

            TISourceView newGroupSource = group.First();

            groups[newGroupSource] = group;

            return true;
        }

        public bool ContainsSource(TISourceView source)
        {
            if (source is null)
            {
                throw new InvalidArgumentException(nameof(source), null);
            }

            if (!effects.ContainsKey(source))
            {
                return false;
            }

            TISourceView groupSource = GetGroup(source);
            if (groupSource == null)
            {
                return false;
            }

            return true;
        }

        private TISourceView GetGroup(TISourceView source)
        {
            if (source is null)
            {
                throw new InvalidArgumentException(nameof(source), null);
            }

            foreach (TISourceView groupSource in groups.Keys)
            {
                if (source == groupSource)
                {
                    return groupSource;
                }
                if (belongsToSameGroupFunction(source, groupSource, epsilon))
                {
                    return groupSource;
                }
            }

            return null;
        }
    }
}
