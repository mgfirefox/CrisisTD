using System;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface ITowerView : IVisualView, ITowerModel
    {
        ITowerActionFolder ActionFolder { get; }

        new TowerId Id { get; set; }

        new TowerType Type { get; set; }

        new TargetPriority Priority { get; set; }

        new Vector3 Position { get; set; }
        new Quaternion Orientation { get; set; }

        new Effect RangeEffect { get; set; }

        new int MaxZeroBranchIndex { get; set; }
        new int MaxFirstBranchIndex { get; set; }
        new int MaxSecondBranchIndex { get; set; }
        new BranchLevel Level { get; set; }

        event Action<Effect, ITowerView> EffectApplied;

        event Action FirstBranchUpgraded;
        event Action SecondBranchUpgraded;

        event Action InteractionShown;
        event Action InteractionHidden;

        void ApplyEffect(Effect effect, ITowerView source);

        void UpgradeFirstBranch();
        void UpgradeSecondBranch();

        void ShowInteraction();
        void HideInteraction();
    }
}
