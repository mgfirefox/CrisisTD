namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractAction<TData, TIView> : AbstractPresenter<TData, TIView>,
        IAction<TData>
        where TIView : class, IActionView
        where TData : AbstractActionData
    {
        protected AbstractAction(TIView view, Scene scene) : base(view, scene)
        {
        }

        public abstract void Perform();
    }
}
