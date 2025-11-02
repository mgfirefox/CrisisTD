using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractTargetService<TITargetView> :
        AbstractDataService<TargetServiceData>, ITargetService<TITargetView>, ISceneStartedListener,
        ISceneFinishedListener
        where TITargetView : class, IView, ITransformModel
    {
        private readonly ITowerTransformService transformService;

        private readonly ITowerAllEffectService effectService;

        private readonly IRangeView<TITargetView> rangeView;

        private readonly IDictionary<TargetType, ISet<TITargetView>> targets =
            new Dictionary<TargetType, ISet<TITargetView>>();
        private readonly IDictionary<TITargetView, Action> targetDestroyingActions =
            new Dictionary<TITargetView, Action>();
        private readonly IDictionary<TITargetView, TargetType> types =
            new Dictionary<TITargetView, TargetType>();

        private readonly IList<TITargetView> changedActiveTargets = new List<TITargetView>();
        private readonly IList<TITargetView> changedPossibleTargets = new List<TITargetView>();

        private float originalRangeRadius;

        public float RangeRadius { get; private set; }

        public TargetPriority TargetPriority { get; private set; }

        public IReadOnlyList<TITargetView> Targets
        {
            get
            {
                ISet<TITargetView> activeTargets = targets[TargetType.Active];

                IList<TITargetView> sortedTargets = SortTargetsByPriority(activeTargets.ToList());
                if (sortedTargets.Count != activeTargets.Count)
                {
                    // TODO: Change Exception
                    throw new Exception("Target Count does not match after sort operation.");
                }

                return sortedTargets.AsReadOnly();
            }
        }

        private bool IsActiveTarget(TITargetView target)
        {
            float epsilon = Scene.Settings.MathSettings.Epsilon;

            float distance = Vector3.Distance(transformService.Position, target.Position);

            return distance.IsLessThanOrEqualApproximately(RangeRadius, epsilon);
        }

        protected virtual bool ShouldTargetBeExcluded(TITargetView target)
        {
            return false;
        }

        public event Action<float> RangeRadiusChanged;

        protected AbstractTargetService(ITowerTransformService transformService,
            ITowerAllEffectService effectService, IRangeView<TITargetView> rangeView,
            Scene scene) : base(scene)
        {
            this.transformService = transformService;
            this.effectService = effectService;
            this.rangeView = rangeView;
        }

        public void OnSceneStarted()
        {
            rangeView.TargetEntered += OnRangeTargetEntered;
            rangeView.TargetExited += OnRangeTargetExited;

            effectService.RangeChanged += OnRangeChanged;
        }

        public void OnSceneFinished()
        {
            rangeView.TargetEntered -= OnRangeTargetEntered;
            rangeView.TargetExited -= OnRangeTargetExited;

            effectService.RangeChanged -= OnRangeChanged;
        }

        public void Update()
        {
            changedActiveTargets.Clear();
            changedPossibleTargets.Clear();

            foreach (TITargetView possibleTarget in targets[TargetType.Possible])
            {
                if (!IsActiveTarget(possibleTarget))
                {
                    continue;
                }

                changedPossibleTargets.Add(possibleTarget);
            }

            foreach (TITargetView activeTarget in targets[TargetType.Active])
            {
                if (IsActiveTarget(activeTarget))
                {
                    continue;
                }

                changedActiveTargets.Add(activeTarget);
            }

            foreach (TITargetView changedPossibleTarget in changedPossibleTargets)
            {
                RemoveTarget(changedPossibleTarget);
                AddTarget(TargetType.Active, changedPossibleTarget);
            }

            foreach (TITargetView changedActiveTarget in changedActiveTargets)
            {
                RemoveTarget(changedActiveTarget);
                AddTarget(TargetType.Possible, changedActiveTarget);
            }

            changedActiveTargets.Clear();
            changedPossibleTargets.Clear();
        }

        public IList<TITargetView> SortFarthestTargets(IList<TITargetView> targets)
        {
            if (targets == null)
            {
                throw new InvalidArgumentException(nameof(targets), null);
            }

            return targets.OrderByDescending(SqrDistanceSelector).ToList();
        }

        public IList<TITargetView> SortClosestTargets(IList<TITargetView> targets)
        {
            if (targets == null)
            {
                throw new InvalidArgumentException(nameof(targets), null);
            }

            return targets.OrderBy(SqrDistanceSelector).ToList();
        }

        public IList<TITargetView> SortRandomTargets(IList<TITargetView> targets)
        {
            if (targets == null)
            {
                throw new InvalidArgumentException(nameof(targets), null);
            }

            return targets.OrderByDescending(RandomTargetSelector).ToList();
        }

        protected virtual IList<TITargetView> SortTargetsByPriority(IList<TITargetView> targets)
        {
            if (targets == null)
            {
                throw new InvalidArgumentException(nameof(targets), null);
            }

            if (!TargetPriorityValidator.TryValidate(TargetPriority))
            {
                // TODO: Change Exception
                throw new Exception();
            }

            return targets;
        }

        private void AddTarget(TargetType type, TITargetView target)
        {
            if (!TargetTypeValidator.TryValidate(type))
            {
                throw new InvalidArgumentException(nameof(type), type.ToString());
            }

            Action targetDestroyingAction = () => RemoveTarget(target);

            target.Destroying += targetDestroyingAction;

            targets[type].Add(target);
            targetDestroyingActions[target] = targetDestroyingAction;
            types[target] = type;
        }

        private void RemoveTarget(TITargetView target)
        {
            if (types.TryGetValue(target, out TargetType type))
            {
                ISet<TITargetView> targets = this.targets[type];
                if (!targets.Contains(target))
                {
                    return;
                }

                target.Destroying -= targetDestroyingActions[target];

                targets.Remove(target);
                targetDestroyingActions.Remove(target);
                types.Remove(target);
            }
        }

        private float SqrDistanceSelector(TITargetView target)
        {
            return Vector3Utility.GetSqrDistance(transformService.Position, target.Position);
        }

        private float RandomTargetSelector(TITargetView target)
        {
            return Random.Range(float.MinValue, float.MaxValue);
        }

        protected override void OnInitialized(TargetServiceData data)
        {
            base.OnInitialized(data);

            originalRangeRadius = data.RangeRadius;
            RangeRadius = originalRangeRadius;
            TargetPriority = data.TargetPriority;

            SetupTargets();

            rangeView.Initialize();

            rangeView.Radius = RangeRadius;
        }

        private void SetupTargets()
        {
            foreach (TargetType validType in TargetTypeValidator.ValidTypes)
            {
                targets[validType] = new HashSet<TITargetView>();
            }
        }

        protected override void OnDestroying()
        {
            rangeView.Destroy();

            ClearTargets();

            base.OnDestroying();
        }

        private void ClearTargets()
        {
            foreach ((TITargetView target, Action action) in targetDestroyingActions)
            {
                target.Destroying -= action;
            }
            targetDestroyingActions.Clear();

            foreach (TargetType validType in TargetTypeValidator.ValidTypes)
            {
                targets[validType].Clear();
            }

            changedPossibleTargets.Clear();
        }

        private void OnRangeTargetEntered(TITargetView target)
        {
            if (ShouldTargetBeExcluded(target))
            {
                AddTarget(TargetType.Excluded, target);

                return;
            }
            if (IsActiveTarget(target))
            {
                AddTarget(TargetType.Active, target);

                return;
            }

            AddTarget(TargetType.Possible, target);
        }

        private void OnRangeTargetExited(TITargetView target)
        {
            RemoveTarget(target);
        }

        private void OnRangeChanged(Effect effect)
        {
            RangeRadius = originalRangeRadius * effect.Value;

            rangeView.Radius = RangeRadius;

            RangeRadiusChanged?.Invoke(RangeRadius);
        }
    }
}
