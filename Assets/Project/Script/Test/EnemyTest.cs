using System.Collections.Generic;
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

        private readonly IList<EnemyId> idList = new List<EnemyId>
        {
            EnemyId.TestWalk, EnemyId.TestRun,
        };
        private int index;

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
                if (index == idList.Count - 1)
                {
                    return;
                }

                index++;

                isFinished = false;

                count = 0;
                cooldown = maxCooldown;
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

            enemyService.TrySpawn(idList[index], mapService.EnemySpawnPose, out IEnemyView _);
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
