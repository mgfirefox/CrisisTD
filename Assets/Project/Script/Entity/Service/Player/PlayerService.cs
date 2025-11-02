using System;
using UnityEngine;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class PlayerService : AbstractService, IPlayerService
    {
        private readonly IMapService mapService;

        private readonly IPlayerFactory factory;

        [Inject]
        public PlayerService(IPlayerFactory factory, IMapService mapService)
        {
            this.mapService = mapService;
            this.factory = factory;
        }

        public IPlayerView Spawn()
        {
            if (!mapService.IsLoaded)
            {
                throw new MapNotLoadedException();
            }

            Pose spawnPose = mapService.PlayerSpawnPose;

            try
            {
                IPlayerView player = factory.Create(spawnPose.position, spawnPose.rotation);

                return player;
            }
            catch (Exception e)
            {
                if (e is not (InvalidArgumentException or ConfigurationNotFoundException))
                {
                    throw new CaughtUnexpectedCheckedException(e);
                }

                throw new SpawnFailedException("Player", e);
            }
        }

        public bool TrySpawn(out IPlayerView view)
        {
            try
            {
                view = Spawn();

                return true;
            }
            catch (Exception e)
            {
                if (e is not (InvalidArgumentException or PrefabNotFoundException))
                {
                    Debug.LogException(new CaughtUnexpectedException(e));
                }
            }

            view = null;

            return false;
        }
    }
}
