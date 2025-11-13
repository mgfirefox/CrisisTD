namespace Mgfirefox.CrisisTd
{
    public class AbstractUiPresenter<TData, TIView, TIUi> : AbstractPresenter<TData, TIView>,
        IUiPresenter<TData>
        where TData : AbstractData
        where TIView : class, IView
        where TIUi : class, IUi
    {
        protected TIUi Ui { get; private set; }

        protected AbstractUiPresenter(TIView view, TIUi ui, Scene scene) : base(view, scene)
        {
            Ui = ui;
        }

        protected override void OnInitialized(TData data)
        {
            base.OnInitialized(data);

            Ui.Initialize();
        }
    }
}
