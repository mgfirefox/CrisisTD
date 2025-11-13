namespace Mgfirefox.CrisisTd
{
    public abstract class
        AbstractUiAction<TData, TIView, TIUi> : AbstractUiPresenter<TData, TIView, TIUi>,
        IUiAction<TData>
        where TData : AbstractActionData
        where TIView : class, IActionView
        where TIUi : class, IActionUi
    {
        protected AbstractUiAction(TIView view, TIUi ui, Scene scene) : base(view, ui, scene)
        {
        }

        public abstract void Perform();
    }
}
