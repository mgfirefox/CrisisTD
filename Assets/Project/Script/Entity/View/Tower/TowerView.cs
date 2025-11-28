using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class TowerView : AbstractView, ITowerView
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private TowerActionFolder actionFolder;

        [SerializeField]
        [BoxGroup("Tower")]
        [ReadOnly]
        private TowerId id;

        [SerializeField]
        [BoxGroup("Tower")]
        [ReadOnly]
        private TowerType type;

        [SerializeField]
        [BoxGroup("Tower")]
        [ReadOnly]
        private TargetPriority priority;

        [SerializeField]
        [BoxGroup("Tower")]
        [ReadOnly]
        private float totalCost;

        [SerializeField]
        [BoxGroup("Transform")]
        [ReadOnly]
        private Vector3 position;
        [SerializeField]
        [BoxGroup("Transform")]
        [ReadOnly]
        private Quaternion orientation;
        
        [SerializeField]
        [BoxGroup("Transform")]
        [ReadOnly]
        private Quaternion visualOrientation;
        [SerializeField]
        [BoxGroup("Transform")]
        [ReadOnly]
        private Quaternion obstacleOrientation;

        [SerializeField]
        [BoxGroup("Effect")]
        [ReadOnly]
        private Effect rangeEffect;

        [SerializeField]
        [BoxGroup("Level")]
        [ReadOnly]
        private int maxZeroBranchIndex;
        [SerializeField]
        [BoxGroup("Level")]
        [ReadOnly]
        private int maxFirstBranchIndex;
        [SerializeField]
        [BoxGroup("Level")]
        [ReadOnly]
        private int maxSecondBranchIndex;
        [SerializeField]
        [BoxGroup("Level")]
        [ReadOnly]
        private BranchLevel level;
        
        [SerializeField]
        [BoxGroup("Level")]
        [ReadOnly]
        private List<NextBranchLevel> nextLevels = new();

        public ITowerActionFolder ActionFolder => actionFolder;

        public TowerId Id { get => id; set => id = value; }

        public TowerType Type { get => type; set => type = value; }

        public TargetPriority Priority { get => priority; set => priority = value; }

        public float TotalCost { get => totalCost; set => totalCost = value; }

        public Vector3 Position
        {
            get
            {
                if (IsDestroyed)
                {
                    return position;
                }

                position = Transform.position;

                return position;
            }
            set
            {
                if (IsDestroyed)
                {
                    position = value;

                    return;
                }

                Transform.position = value;

                position = Transform.position;
            }
        }
        public Quaternion Orientation
        {
            get
            {
                if (IsDestroyed)
                {
                    return orientation;
                }

                orientation = Transform.rotation;

                return orientation;
            }
            set
            {
                if (IsDestroyed)
                {
                    orientation = value;

                    return;
                }

                Transform.rotation = value;

                orientation = Transform.rotation;
            }
        }

        public Quaternion VisualOrientation
        {
            get => visualOrientation;
            set
            {
                visualOrientation = value;

                if (IsDestroyed)
                {
                    return;
                }

                Model.Transform.rotation = visualOrientation;
            }
        }
        public Quaternion ObstacleOrientation
        {
            get => obstacleOrientation;
            set => obstacleOrientation = value;
        }

        protected override IUnitySceneObject GetChildParent(IUnitySceneObject child)
        {
            if (child.Transform.TryGetComponent(out ITowerActionView _))
            {
                return actionFolder;
            }

            return base.GetChildParent(child);
        }

        public Effect RangeEffect { get => rangeEffect; set => rangeEffect = value; }

        public int MaxZeroBranchIndex
        {
            get => maxZeroBranchIndex;
            set => maxZeroBranchIndex = value;
        }
        public int MaxFirstBranchIndex
        {
            get => maxFirstBranchIndex;
            set => maxFirstBranchIndex = value;
        }
        public int MaxSecondBranchIndex
        {
            get => maxSecondBranchIndex;
            set => maxSecondBranchIndex = value;
        }
        public BranchLevel Level { get => level; set => level = value; }

        IReadOnlyList<NextBranchLevel> ILevelModel.NextLevels => nextLevels.AsReadOnly();
        public IList<NextBranchLevel> NextLevels => nextLevels;

        public event Action<Effect, ITowerView> EffectApplied;

        public event Action FirstBranchUpgraded;
        public event Action SecondBranchUpgraded;

        public event Action InteractionShown;
        public event Action InteractionHidden;

        public void ApplyEffect(Effect effect, ITowerView source)
        {
            EffectApplied?.Invoke(effect, source);
        }

        public void UpgradeFirstBranch()
        {
            FirstBranchUpgraded?.Invoke();
        }

        public void UpgradeSecondBranch()
        {
            SecondBranchUpgraded?.Invoke();
        }

        public void ShowInteraction()
        {
            InteractionShown?.Invoke();
        }

        public void HideInteraction()
        {
            InteractionHidden?.Invoke();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            actionFolder.Initialize();
        }

        protected override void OnDestroying()
        {
            actionFolder.Destroy();

            base.OnDestroying();
        }
    }
}
