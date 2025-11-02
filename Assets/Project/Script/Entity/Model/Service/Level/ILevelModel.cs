namespace Mgfirefox.CrisisTd
{
    public interface ILevelModel : IModel
    {
        int MaxBranch0Index { get; }
        int MaxBranch1Index { get; }
        int MaxBranch2Index { get; }
        LevelIndex Index { get; }
    }
}
