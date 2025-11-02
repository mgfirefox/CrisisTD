using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class TowerService : AbstractService, ITowerService
    {
        private readonly ITowerFactory factory;

        private readonly IList<ITowerView> towers = new List<ITowerView>();
        private readonly IDictionary<ITowerView, Action> towerDestroyingActions =
            new Dictionary<ITowerView, Action>();

        public IReadOnlyList<ITowerView> Towers => towers.AsReadOnly();
        public int Count => towers.Count;

        [Inject]
        public TowerService(ITowerFactory factory)
        {
            this.factory = factory;
        }

        public ITowerView Spawn(TowerId id, Vector3 position, Quaternion orientation)
        {
            try
            {
                ITowerView tower = factory.Create(id, position, orientation);

                Action towerDestroyingAction = () => OnTowerDestroying(tower);

                tower.Destroying += towerDestroyingAction;

                towers.Add(tower);
                towerDestroyingActions[tower] = towerDestroyingAction;

                return tower;
            }
            catch (InvalidArgumentException e)
            {
                throw new RestorableInvalidArgumentException(nameof(id), id.ToString(), e);
            }
            catch (Exception e)
            {
                if (e is not ConfigurationNotFoundException)
                {
                    throw new CaughtUnexpectedCheckedException(e);
                }

                throw new SpawnFailedException("Tower", e);
            }
        }

        public bool TrySpawn(TowerId id, Vector3 position, Quaternion orientation,
            out ITowerView view)
        {
            try
            {
                view = Spawn(id, position, orientation);

                return true;
            }
            catch (Exception e)
            {
                if (e is not (RestorableInvalidArgumentException or InvalidArgumentException
                    or PrefabNotFoundException))
                {
                    Debug.LogException(new CaughtUnexpectedException(e));
                }
            }

            view = null;

            return false;
        }

        private void OnTowerDestroying(ITowerView tower)
        {
            tower.Destroying -= towerDestroyingActions[tower];

            towers.Remove(tower);
            towerDestroyingActions.Remove(tower);
        }
    }
}
