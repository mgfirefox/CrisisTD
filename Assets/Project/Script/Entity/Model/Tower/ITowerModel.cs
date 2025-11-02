namespace Mgfirefox.CrisisTd
{
    public interface ITowerModel : IModel, ITransformModel, IRangeEffectModel, ILevelModel
    {
        TowerId Id { get; }

        TowerType Type { get; }

        TargetPriority Priority { get; }
    }
}
