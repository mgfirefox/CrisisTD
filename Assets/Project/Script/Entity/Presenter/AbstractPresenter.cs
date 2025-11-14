namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractPresenter<TData, TIView> : AbstractSceneObject<TData>,
        IPresenter<TData>, ISceneStartedListener, ISceneFinishedListener
        where TData : AbstractData
        where TIView : class, IView
    {
        protected TIView View { get; }

        private bool isViewDestroying;

        protected AbstractPresenter(TIView view, Scene scene) : base(scene)
        {
            View = view;
        }

        public virtual void OnSceneStarted()
        {
            View.Destroying += OnViewDestroying;
        }

        public virtual void OnSceneFinished()
        {
            View.Destroying -= OnViewDestroying;
        }

        protected override void OnInitialized(TData data)
        {
            base.OnInitialized(data);

            View.Initialize();
        }

        protected override void OnDestroying()
        {
            if (!isViewDestroying)
            {
                View.Destroy();
            }

            base.OnDestroying();
        }

        private void OnViewDestroying()
        {
            isViewDestroying = true;

            Destroy();
        }
    }
}
