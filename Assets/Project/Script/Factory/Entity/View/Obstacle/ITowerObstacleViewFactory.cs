namespace Mgfirefox.CrisisTd
{
    public interface ITowerObstacleViewFactory
    {
        ITowerObstacleView Create();
        bool TryCreate(out ITowerObstacleView view);
    }
}
