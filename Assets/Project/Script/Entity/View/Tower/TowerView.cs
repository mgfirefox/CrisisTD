using System;
using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class TowerView : AbstractVisualView, ITowerView
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private TowerActionFolderView actionViewFolder;

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
        [BoxGroup("Transform")]
        [ReadOnly]
        private Vector3 position;
        [SerializeField]
        [BoxGroup("Transform")]
        [ReadOnly]
        private Quaternion orientation;

        [SerializeField]
        [BoxGroup("Effect")]
        [ReadOnly]
        private Effect rangeEffect;

        [SerializeField]
        [BoxGroup("Level")]
        [ReadOnly]
        private int maxBranch0Index;
        [SerializeField]
        [BoxGroup("Level")]
        [ReadOnly]
        private int maxBranch1Index;
        [SerializeField]
        [BoxGroup("Level")]
        [ReadOnly]
        private int maxBranch2Index;
        [SerializeField]
        [BoxGroup("Level")]
        [ReadOnly]
        private LevelIndex index;

        public ITowerActionFolderView ActionViewFolder => actionViewFolder;

        public TowerId Id { get => id; set => id = value; }

        public TowerType Type { get => type; set => type = value; }

        public TargetPriority Priority { get => priority; set => priority = value; }

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

        protected override IUnitySceneObject GetChildParent(IUnitySceneObject child)
        {
            if (child.Transform.TryGetComponent(out ITowerActionView _))
            {
                return actionViewFolder;
            }

            return base.GetChildParent(child);
        }

        public Effect RangeEffect { get => rangeEffect; set => rangeEffect = value; }

        public int MaxBranch0Index { get => maxBranch0Index; set => maxBranch0Index = value; }
        public int MaxBranch1Index { get => maxBranch1Index; set => maxBranch1Index = value; }
        public int MaxBranch2Index { get => maxBranch2Index; set => maxBranch2Index = value; }
        public LevelIndex Index { get => index; set => index = value; }

        public event Action<Effect, ITowerView> EffectApplied;

        public event Action Branch1Upgraded;
        public event Action Branch2Upgraded;

        public event Action InteractionShown;
        public event Action InteractionHidden;

        public void ApplyEffect(Effect effect, ITowerView source)
        {
            EffectApplied?.Invoke(effect, source);
        }

        public void UpgradeBranch1()
        {
            Branch1Upgraded?.Invoke();
        }

        public void UpgradeBranch2()
        {
            Branch2Upgraded?.Invoke();
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

            actionViewFolder.Initialize();
        }

        protected override void OnDestroying()
        {
            actionViewFolder.Destroy();

            base.OnDestroying();
        }
    }
}
