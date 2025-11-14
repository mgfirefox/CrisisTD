namespace Mgfirefox.CrisisTd
{
    public interface IArcAngleAttackActionModel : IAttackActionModel
    {
        public float ArcAngle { get; }

        public int MaxPelletCount { get; }
        public int MaxPelletHitCount { get; }
    }
}
