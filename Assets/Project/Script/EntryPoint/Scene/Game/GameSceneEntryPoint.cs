using VContainer;
using VContainer.Unity;

namespace Mgfirefox.CrisisTd
{
    public class GameSceneEntryPoint : IInitializable
    {
        private readonly IMapService mapService;

        private readonly IBaseService baseService;

        private readonly IPlayerService playerService;
        
        private readonly IEconomyService economyService;

        private readonly EnemyTest enemyTest;

        [Inject]
        public GameSceneEntryPoint(IMapService mapService, IBaseService baseService,
            IPlayerService playerService, IEconomyService economyService, EnemyTest enemyTest)
        {
            this.mapService = mapService;
            this.baseService = baseService;
            this.playerService = playerService;
            this.economyService = economyService;
            this.enemyTest = enemyTest;
        }

        public void Initialize()
        {
            mapService.Load(MapId.Test);

            baseService.Spawn();

            playerService.Spawn();
            
            economyService.Initialize(100000.0f);

            enemyTest.Initialize();
        }
    }
}
