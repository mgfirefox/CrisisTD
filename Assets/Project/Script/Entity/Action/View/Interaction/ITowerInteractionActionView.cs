namespace Mgfirefox.CrisisTd
{
    public interface ITowerInteractionActionView : IActionView, ITowerInteractionActionModel
    {
        new bool IsInteracting { get; set; }
    }
}
