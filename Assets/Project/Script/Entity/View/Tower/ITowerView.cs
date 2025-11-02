using System;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface ITowerView : IVisualView, ITowerModel
    {
        ITowerActionFolderView ActionViewFolder { get; }

        new TowerId Id { get; set; }

        new TowerType Type { get; set; }

        new TargetPriority Priority { get; set; }

        new Vector3 Position { get; set; }
        new Quaternion Orientation { get; set; }

        new Effect RangeEffect { get; set; }

        new int MaxBranch0Index { get; set; }
        new int MaxBranch1Index { get; set; }
        new int MaxBranch2Index { get; set; }
        new LevelIndex Index { get; set; }

        event Action<Effect, ITowerView> EffectApplied;

        event Action Branch1Upgraded;
        event Action Branch2Upgraded;

        event Action InteractionShown;
        event Action InteractionHidden;

        void ApplyEffect(Effect effect, ITowerView source);

        void UpgradeBranch1();
        void UpgradeBranch2();

        void ShowInteraction();
        void HideInteraction();
    }
}
