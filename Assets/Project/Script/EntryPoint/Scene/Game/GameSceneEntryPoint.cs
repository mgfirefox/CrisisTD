using VContainer;
using VContainer.Unity;

namespace Mgfirefox.CrisisTd
{
    public class GameSceneEntryPoint : IInitializable
    {
        private readonly IMapService mapService;

        private readonly IBaseService baseService;

        private readonly IPlayerService playerService;

        private readonly EnemyTest enemyTest;

        [Inject]
        public GameSceneEntryPoint(IMapService mapService, IBaseService baseService,
            IPlayerService playerService, EnemyTest enemyTest)
        {
            this.mapService = mapService;
            this.baseService = baseService;
            this.playerService = playerService;
            this.enemyTest = enemyTest;
        }

        public void Initialize()
        {
            mapService.Load(MapId.Test);

            baseService.Spawn();

            playerService.Spawn();

            enemyTest.Initialize();
        }
    }
}
