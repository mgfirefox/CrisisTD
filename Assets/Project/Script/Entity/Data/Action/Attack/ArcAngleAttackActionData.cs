namespace Mgfirefox.CrisisTd
{
    public class ArcAngleAttackActionData : AbstractAttackActionData
    {
        public override AttackActionType Type => AttackActionType.ArcAngle;

        public float ArcAngle { get; set; }

        public int PelletCount { get; set; }
        public int MaxPelletHitCount { get; set; }
    }
}
