namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractAction<TData, TIView> : AbstractPresenter<TData, TIView>,
        IAction<TData>
        where TData : AbstractActionData
        where TIView : class, IActionView
    {
        protected AbstractAction(TIView view, Scene scene) : base(view, scene)
        {
        }

        public abstract void Perform();
    }
}
