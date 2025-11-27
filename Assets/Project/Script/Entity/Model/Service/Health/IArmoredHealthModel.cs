namespace Mgfirefox.CrisisTd
{
    public interface IArmoredHealthModel : IHealthModel
    {
        float Shield { get; }
        float Armor { get; }
    }
}
