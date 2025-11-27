using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class EnemyPresenter : AbstractPresenter<EnemyData, IEnemyView>, IEnemyPresenter,
        ISceneTickedListener
    {
        private readonly IBaseView @base;

        private readonly IEnemyTransformService transformService;

        private readonly IArmoredHealthService healthService;

        [Inject]
        public EnemyPresenter(IEnemyView view, IEnemyTransformService transformService,
            IArmoredHealthService healthService, IBaseService baseService, Scene scene) : base(view, scene)
        {
            this.transformService = transformService;
            this.healthService = healthService;
            @base = baseService.Get(0);
        }

        public override void OnSceneStarted()
        {
            base.OnSceneStarted();

            View.DamageTaken += OnDamageTaken;
            View.Died += OnDied;

            transformService.BaseReached += OnBaseReached;

            healthService.Died += OnHealthDied;
        }

        public override void OnSceneFinished()
        {
            base.OnSceneFinished();

            View.DamageTaken -= OnDamageTaken;
            View.Died -= OnDied;

            transformService.BaseReached -= OnBaseReached;

            healthService.Died -= OnHealthDied;
        }

        public void OnSceneTicked()
        {
            Move();
        }

        private void Move()
        {
            transformService.Move();

            View.Position = transformService.Position;
            View.Orientation = transformService.Orientation;
            View.MovementSpeed = transformService.MovementSpeed;
            View.WaypointIndex = transformService.WaypointIndex;
        }

        protected override void OnInitialized(EnemyData data)
        {
            base.OnInitialized(data);

            transformService.Initialize(data.TransformServiceData);
            transformService.PivotPoint = View.PivotPoint;

            View.InitialPosition = transformService.Position;
            View.InitialOrientation = transformService.Orientation;
            View.MaxMovementSpeed = transformService.MaxMovementSpeed;
            View.MovementSpeed = transformService.MovementSpeed;
            View.WaypointIndex = transformService.WaypointIndex;

            healthService.Initialize(data.ArmoredHealthServiceData);

            View.MaxHealth = healthService.MaxHealth;
            View.Health = healthService.Health;
            View.Shield = healthService.Shield;
            View.Armor = healthService.Armor;
        }

        protected override void OnDestroying()
        {
            healthService.Destroy();

            transformService.Destroy();

            base.OnDestroying();
        }

        private void OnBaseReached()
        {
            @base.TakeDamage(View);
        }

        private void OnHealthDied()
        {
            View.Die();

            Destroy();
        }

        private void OnDamageTaken(float damage, float armorPiercing)
        {
            healthService.TakeDamage(damage, armorPiercing);

            View.Health = healthService.Health;
            View.Shield = healthService.Shield;
        }

        private void OnDied()
        {
            healthService.Die();

            View.Health = healthService.Health;
            View.IsDied = healthService.IsDied;
        }
    }
}
