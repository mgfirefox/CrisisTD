namespace Mgfirefox.CrisisTd
{
    public interface ITimeService : IService
    {
        float DeltaTime { get; }
        float FixedDeltaTime { get; }
    }
}
