namespace Mgfirefox.CrisisTd
{
    public class AreaAttackActionData : AbstractAttackActionData
    {
        public override AttackActionType Type => AttackActionType.Area;

        public float InnerRadius { get; set; }
        public float OuterRadius { get; set; }

        public int MaxHitCount { get; set; }
    }
}
