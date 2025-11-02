namespace Mgfirefox.CrisisTd
{
    public class AbstractTowerAction<TData, TIView, TITargetView, TITargetService> :
        AbstractAction<TData, TIView>, ITowerAction<TData>
        where TData : AbstractTowerActionData
        where TIView : class, ITowerActionView
        where TITargetView : class, IView
        where TITargetService : class, ITargetService<TITargetView>
    {
        private readonly TITargetService targetService;
        private readonly IRangeView rangeView;

        protected AbstractTowerAction(TIView view, TITargetService targetService,
            IRangeView rangeView, Scene scene) : base(view, scene)
        {
            this.targetService = targetService;
            this.rangeView = rangeView;
        }

        public override void OnSceneStarted()
        {
            base.OnSceneStarted();

            targetService.RangeRadiusChanged += OnRangeRadiusChanged;
        }

        public override void OnSceneFinished()
        {
            base.OnSceneFinished();

            targetService.RangeRadiusChanged -= OnRangeRadiusChanged;
        }

        public override void Perform()
        {
            targetService.Update();
        }

        public void ShowInteraction()
        {
            rangeView.Show();
        }

        public void HideInteraction()
        {
            rangeView.Hide();
        }

        protected override void OnInitialized(TData data)
        {
            base.OnInitialized(data);

            targetService.Initialize(data.TargetServiceData);

            View.RangeRadius = targetService.RangeRadius;
            View.TargetPriority = targetService.TargetPriority;
        }

        protected override void OnDestroying()
        {
            targetService.Destroy();

            base.OnDestroying();
        }

        private void OnRangeRadiusChanged(float rangeRadius)
        {
            View.RangeRadius = rangeRadius;
        }
    }
}
