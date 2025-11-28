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
        
        private readonly IList<NextBranchLevel> nextLevels = new List<NextBranchLevel>();

        public int MaxZeroBranchIndex { get; private set; }
        public int MaxFirstBranchIndex { get; private set; }
        public int MaxSecondBranchIndex { get; private set; }
        public BranchLevel Level { get; private set; } = new();

        public IReadOnlyList<NextBranchLevel> NextLevels => nextLevels.AsReadOnly();

        public event Action<LevelItem> Changed;

        [Inject]
        public LevelService(Scene scene) : base(scene)
        {
        }

        public void UpgradeFirstBranch()
        {
            if (Level.Type == BranchType.Zero)
            {
                if (MaxZeroBranchIndex < 0)
                {
                    return;
                }

                if (Level.Index < MaxZeroBranchIndex)
                {
                    Level.Index++;

                    nextLevels.Clear();
                    if (Level.Index < MaxZeroBranchIndex)
                    {
                        AddNextLevel();
                    }
                    else
                    {
                        AddNextLevels();
                    }

                    InvokeChanged();

                    return;
                }

                if (MaxFirstBranchIndex < 0)
                {
                    return;
                }

                Level = new BranchLevel(BranchType.First);

                nextLevels.Clear();
                if (Level.Index < MaxFirstBranchIndex)
                {
                    AddNextLevel();
                }

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

            Level.Index++;
            
            nextLevels.Clear();
            if (Level.Index < MaxFirstBranchIndex)
            {
                AddNextLevel();
            }

            InvokeChanged();
        }

        public void UpgradeSecondBranch()
        {
            if (Level.Type == BranchType.Zero)
            {
                if (MaxZeroBranchIndex < 0)
                {
                    return;
                }

                if (Level.Index < MaxZeroBranchIndex)
                {
                    Level.Index++;

                    nextLevels.Clear();
                    if (Level.Index < MaxZeroBranchIndex)
                    {
                        AddNextLevel();
                    }
                    else
                    {
                        AddNextLevels();
                    }
                    
                    InvokeChanged();

                    return;
                }

                if (MaxSecondBranchIndex < 0)
                {
                    return;
                }

                Level = new BranchLevel(BranchType.Second);
                
                nextLevels.Clear();
                if (Level.Index < MaxSecondBranchIndex)
                {
                    AddNextLevel();
                }

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

            Level.Index++;
            
            nextLevels.Clear();
            if (Level.Index < MaxSecondBranchIndex)
            {
                AddNextLevel();
            }

            InvokeChanged();
        }

        public LevelItem Get(BranchLevel level)
        {
            if (items.TryGetValue(level, out LevelItem item))
            {
                return item;
            }

            return new LevelItem();
        }
        
        private void AddNextLevel()
        {
            var nextLevel = new NextBranchLevel(Level.Type)
            {
                Index = Level.Index + 1,
            };

            var level = new BranchLevel(nextLevel.Type)
            {
                Index = nextLevel.Index,
            };
            
            nextLevel.UpgradeCost = items[level].UpgradeCost;

            nextLevels.Add(nextLevel);
        }
        
        private void AddNextLevels()
        {
            var nextLevel = new NextBranchLevel(BranchType.First);
            if (nextLevel.Index <= MaxFirstBranchIndex)
            {
                var level = new BranchLevel(nextLevel.Type)
                {
                    Index = nextLevel.Index,
                };
            
                nextLevel.UpgradeCost = items[level].UpgradeCost;
                
                nextLevels.Add(nextLevel);
            }

            nextLevel = new NextBranchLevel(BranchType.Second);
            if (nextLevel.Index <= MaxSecondBranchIndex)
            {
                var level = new BranchLevel(nextLevel.Type)
                {
                    Index = nextLevel.Index,
                };
            
                nextLevel.UpgradeCost = items[level].UpgradeCost;
                
                nextLevels.Add(nextLevel);
            }
        }

        private void InvokeChanged()
        {
            LevelItem item = Get(Level);

            Changed?.Invoke(item.Clone() as LevelItem);
        }
        
        private void InvokeChanged(BranchLevel level)
        {
            LevelItem item = Get(level);

            Changed?.Invoke(item.Clone() as LevelItem);
        }

        protected override void OnInitialized(LevelServiceData data)
        {
            base.OnInitialized(data);

            MaxZeroBranchIndex = 0;
            MaxFirstBranchIndex = 0;
            MaxSecondBranchIndex = 0;

            InitializeItems(data.Items);

            InitializeLevel(data.Level);
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

            for (var level = new BranchLevel(); level.Index <= MaxZeroBranchIndex; level.Index++)
            {
                if (items.ContainsKey(level))
                {
                    continue;
                }

                MaxZeroBranchIndex = level.Index - 1;

                break;
            }

            for (var level = new BranchLevel(BranchType.First);
                 level.Index <= MaxFirstBranchIndex;
                 level.Index++)
            {
                if (items.ContainsKey(level))
                {
                    continue;
                }

                MaxFirstBranchIndex = level.Index - 1;

                break;
            }

            for (var level = new BranchLevel(BranchType.Second);
                 level.Index <= MaxSecondBranchIndex;
                 level.Index++)
            {
                if (items.ContainsKey(level))
                {
                    continue;
                }

                MaxSecondBranchIndex = level.Index - 1;

                break;
            }
        }

        private void InitializeLevel(BranchLevel level)
        {
            Level = (BranchLevel)level.Clone();
            if (!items.ContainsKey(Level))
            {
                Level = new BranchLevel();
            }

            switch (Level.Type)
            {
                case BranchType.Zero:
                    if (Level.Index < MaxZeroBranchIndex)
                    {
                        AddNextLevel();
                    }
                    else
                    {
                        AddNextLevels();
                    }
                    
                    for (var tempLevel = new BranchLevel();
                         tempLevel.Index <= Level.Index;
                         tempLevel.Index++)
                    {
                        InvokeChanged(tempLevel);
                    }
                break;
                case BranchType.First:
                    if (Level.Index < MaxFirstBranchIndex)
                    {
                        AddNextLevel();
                    }
                    
                    for (var tempLevel = new BranchLevel();
                         tempLevel.Index <= MaxZeroBranchIndex;
                         tempLevel.Index++)
                    {
                        InvokeChanged(tempLevel);
                    }
                    for (var tempLevel = new BranchLevel(BranchType.First);
                         tempLevel.Index <= Level.Index;
                         tempLevel.Index++)
                    {
                        InvokeChanged(tempLevel);
                    }
                break;
                case BranchType.Second:
                    if (Level.Index < MaxSecondBranchIndex)
                    {
                        AddNextLevel();
                    }
                    
                    for (var tempLevel = new BranchLevel();
                         tempLevel.Index <= MaxZeroBranchIndex;
                         tempLevel.Index++)
                    {
                        InvokeChanged(tempLevel);
                    }
                    for (var tempLevel = new BranchLevel(BranchType.Second);
                         tempLevel.Index <= Level.Index;
                         tempLevel.Index++)
                    {
                        InvokeChanged(tempLevel);
                    }
                break;
                case BranchType.Undefined:
                default:
                    return;
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
