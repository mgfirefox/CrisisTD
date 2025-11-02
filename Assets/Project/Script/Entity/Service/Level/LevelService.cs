using System;
using System.Collections.Generic;
using Mgfirefox.CrisisTd.Level;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class LevelService : AbstractDataService<LevelServiceData>, ILevelService
    {
        private readonly IDictionary<LevelIndex, LevelItem> dataDictionary =
            new Dictionary<LevelIndex, LevelItem>();

        public int MaxBranch0Index { get; private set; }
        public int MaxBranch1Index { get; private set; }
        public int MaxBranch2Index { get; private set; }
        public LevelIndex Index { get; private set; } = new();

        private bool IsBranch0 =>
            Index.Branch0 >= 0 && Index.Branch0 <= MaxBranch0Index && Index.Branch1 == 0 &&
            Index.Branch2 == 0;
        private bool IsBranch1 =>
            Index.Branch0 == MaxBranch0Index && Index.Branch1 > 0 &&
            Index.Branch1 <= MaxBranch0Index && Index.Branch2 == 0;
        private bool IsBranch2 =>
            Index.Branch0 == MaxBranch0Index && Index.Branch1 == 0 && Index.Branch2 > 0 &&
            Index.Branch2 <= MaxBranch2Index;

        public event Action<LevelItem> Changed;

        [Inject]
        public LevelService(Scene scene) : base(scene)
        {
        }

        public void UpgradeBranch1()
        {
            if (IsBranch0)
            {
                if (Index.Branch0 < MaxBranch0Index)
                {
                    UpgradeBranch0();

                    return;
                }

                Index.Branch1++;

                InvokeChanged();

                return;
            }

            if (!IsBranch1)
            {
                return;
            }
            if (Index.Branch1 == MaxBranch1Index)
            {
                return;
            }

            Index.Branch1++;

            InvokeChanged();
        }

        public void UpgradeBranch2()
        {
            if (IsBranch0)
            {
                if (Index.Branch0 < MaxBranch0Index)
                {
                    UpgradeBranch0();

                    return;
                }

                Index.Branch2++;

                InvokeChanged();

                return;
            }

            if (!IsBranch2)
            {
                return;
            }
            if (Index.Branch2 == MaxBranch2Index)
            {
                return;
            }

            Index.Branch2++;

            InvokeChanged();
        }

        private void UpgradeBranch0()
        {
            Index.Branch0++;

            InvokeChanged();
        }

        private void InvokeChanged()
        {
            LevelItem item = dataDictionary[Index];

            Changed?.Invoke(item);
        }

        protected override void OnInitialized(LevelServiceData data)
        {
            base.OnInitialized(data);

            InitializeDataDictionary(data.dataDictionary);

            Index = data.Index.Clone() as LevelIndex;

            InvokeChanged();
        }

        private void InitializeDataDictionary(IDictionary<LevelIndex, LevelItem> dataDictionary)
        {
            MaxBranch0Index = Constant.maxLevelBranch0Index;
            MaxBranch1Index = Constant.maxLevelBranch1Index;
            MaxBranch2Index = Constant.maxLevelBranch2Index;

            int maxBranch0Index = 0;
            int maxBranch1Index = 0;
            int maxBranch2Index = 0;

            foreach ((LevelIndex index, LevelItem data) in dataDictionary)
            {
                Index = index.Clone() as LevelIndex;

                if (IsBranch0)
                {
                    if (index.Branch0 > maxBranch0Index)
                    {
                        maxBranch0Index = index.Branch0;
                    }
                }
                else if (IsBranch1)
                {
                    if (index.Branch1 > maxBranch1Index)
                    {
                        maxBranch1Index = index.Branch1;
                    }
                }
                else if (IsBranch2)
                {
                    if (index.Branch2 > maxBranch2Index)
                    {
                        maxBranch2Index = index.Branch2;
                    }
                }
                else
                {
                    continue;
                }

                this.dataDictionary[index] = data;
            }

            MaxBranch0Index = maxBranch0Index;
            MaxBranch1Index = maxBranch1Index;
            MaxBranch2Index = maxBranch2Index;
        }

        protected override void OnDestroying()
        {
            ClearDataDictionary();

            base.OnDestroying();
        }

        private void ClearDataDictionary()
        {
            dataDictionary.Clear();
        }
    }
}
