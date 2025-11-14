namespace Mgfirefox.CrisisTd
{
    public interface ITowerPreviewFactory : IFactory
    {
        ITowerPreviewView Create(TowerId id);
        bool TryCreate(TowerId id, out ITowerPreviewView view);
    }
}
