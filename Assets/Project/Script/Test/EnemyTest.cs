using VContainer;

namespace Mgfirefox.CrisisTd
{
    // TODO: Replace it with Service
    public class EnemyTest : AbstractSceneObject, ISceneStartedListener, ISceneTickedListener,
        ISceneFinishedListener
    {
        private readonly ITimeService timeService;

        private readonly IEnemyService enemyService;

        private readonly IMapService mapService;

        private readonly IBaseService baseService;

        private IBaseView @base;

        private float cooldown;
        private int count;

        private const int maxEnemyCount = 10;
        private const float maxCooldown = 0.5f;

        private bool isFinished;

        [Inject]
        public EnemyTest(IEnemyService enemyService, IMapService mapService,
            IBaseService baseService, ITimeService timeService, Scene scene) : base(scene)
        {
            this.enemyService = enemyService;
            this.mapService = mapService;
            this.baseService = baseService;
            this.timeService = timeService;
        }

        public void OnSceneStarted()
        {
            @base = baseService.Get(0);
            @base.Died += OnBaseDied;
        }

        public new void Initialize()
        {
            base.Initialize();
        }

        public void OnSceneTicked()
        {
            if (isFinished)
            {
                return;
            }

            float deltaTime = timeService.DeltaTime;

            cooldown -= deltaTime;
            if (cooldown > 0.0f)
            {
                return;
            }

            if (count >= maxEnemyCount)
            {
                isFinished = true;

                return;
            }
            count++;

            cooldown = maxCooldown;

            enemyService.TrySpawn(mapService.EnemySpawnPose, out IEnemyView _);
        }

        public void OnSceneFinished()
        {
            @base.Died -= OnBaseDied;
            @base = null;
        }

        private void OnBaseDied()
        {
            enemyService.DespawnAll();

            Destroy();
        }
    }
}
