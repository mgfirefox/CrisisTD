namespace Mgfirefox.CrisisTd
{
    public interface IArcAngleAttackActionView : IAttackActionView, IArcAngleAttackActionModel
    {
        new float ArcAngle { get; set; }

        new int MaxPelletCount { get; set; }
        new int MaxPelletHitCount { get; set; }
    }
}
