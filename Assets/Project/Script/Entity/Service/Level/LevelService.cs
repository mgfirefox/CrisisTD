using System;
using System.Collections.Generic;
using Mgfirefox.CrisisTd.Level;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class LevelService : AbstractDataService<LevelServiceData>, ILevelService
    {
        private readonly IDictionary<BranchLevel, LevelItem> items =
            new Dictionary<BranchLevel, LevelItem>();

        public int MaxZeroBranchIndex { get; private set; }
        public int MaxFirstBranchIndex { get; private set; }
        public int MaxSecondBranchIndex { get; private set; }
        public BranchLevel Level { get; private set; } = new();

        public event Action<LevelItem> Changed;

        [Inject]
        public LevelService(Scene scene) : base(scene)
        {
        }

        public void UpgradeFirstBranch()
        {
            if (Level.Type == BranchType.Zero)
            {
                if (Level.Index < MaxZeroBranchIndex)
                {
                    UpgradeBranch();

                    return;
                }

                Level = new BranchLevel(BranchType.First);

                InvokeChanged();

                return;
            }

            if (Level.Type != BranchType.First)
            {
                return;
            }
            if (Level.Index >= MaxFirstBranchIndex)
            {
                return;
            }

            UpgradeBranch();
        }

        public void UpgradeSecondBranch()
        {
            if (Level.Type == BranchType.Zero)
            {
                if (Level.Index < MaxZeroBranchIndex)
                {
                    UpgradeBranch();

                    return;
                }

                Level = new BranchLevel(BranchType.Second);

                InvokeChanged();

                return;
            }

            if (Level.Type != BranchType.Second)
            {
                return;
            }
            if (Level.Index >= MaxSecondBranchIndex)
            {
                return;
            }

            UpgradeBranch();
        }

        private void UpgradeBranch()
        {
            Level.Index++;

            InvokeChanged();
        }

        private void InvokeChanged()
        {
            LevelItem item = items[Level];

            Changed?.Invoke(item.Clone() as LevelItem);
        }

        protected override void OnInitialized(LevelServiceData data)
        {
            base.OnInitialized(data);

            InitializeItems(data.Items);

            Level = data.Level.Clone() as BranchLevel;

            InvokeChanged();
        }

        private void InitializeItems(IDictionary<BranchLevel, LevelItem> items)
        {
            foreach ((BranchLevel level, LevelItem item) in items)
            {
                if (!BranchLevelValidator.TryValidate(level))
                {
                    continue;
                }

                switch (level.Type)
                {
                    case BranchType.Zero:
                        if (level.Index > MaxZeroBranchIndex)
                        {
                            MaxZeroBranchIndex = level.Index;
                        }
                    break;
                    case BranchType.First:
                        if (level.Index > MaxFirstBranchIndex)
                        {
                            MaxFirstBranchIndex = level.Index;
                        }
                    break;
                    case BranchType.Second:
                        if (level.Index > MaxSecondBranchIndex)
                        {
                            MaxSecondBranchIndex = level.Index;
                        }
                    break;
                    case BranchType.Undefined:
                    default:
                        continue;
                }

                this.items[(BranchLevel)level.Clone()] = item.Clone() as LevelItem;
            }
        }

        protected override void OnDestroying()
        {
            ClearItems();

            base.OnDestroying();
        }

        private void ClearItems()
        {
            items.Clear();
        }
    }
}
