using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class EnemyService : AbstractService, IEnemyService
    {
        private readonly IEnemyFactory factory;

        private readonly IList<IEnemyView> enemies = new List<IEnemyView>();
        private readonly IDictionary<IEnemyView, Action> enemyDestroyingActions =
            new Dictionary<IEnemyView, Action>();

        public IReadOnlyList<IEnemyView> Enemies => enemies.AsReadOnly();
        public int Count => enemies.Count;

        [Inject]
        public EnemyService(IEnemyFactory factory)
        {
            this.factory = factory;
        }

        public IEnemyView Spawn(Vector3 position, Quaternion orientation)
        {
            try
            {
                IEnemyView enemy = factory.Create(position, orientation);

                Action enemyDestroyingAction = () => OnEnemyDestroying(enemy);

                enemy.Destroying += enemyDestroyingAction;

                enemies.Add(enemy);
                enemyDestroyingActions[enemy] = enemyDestroyingAction;

                return enemy;
            }
            catch (Exception e)
            {
                if (e is not (InvalidArgumentException or ConfigurationNotFoundException))
                {
                    throw new CaughtUnexpectedCheckedException(e);
                }

                throw new SpawnFailedException("Enemy", e);
            }
        }

        public IEnemyView Spawn(Pose pose)
        {
            return Spawn(pose.position, pose.rotation);
        }

        public bool TrySpawn(Vector3 position, Quaternion orientation, out IEnemyView view)
        {
            try
            {
                view = Spawn(position, orientation);

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

        public bool TrySpawn(Pose pose, out IEnemyView view)
        {
            return TrySpawn(pose.position, pose.rotation, out view);
        }

        public void DespawnAll()
        {
            foreach (IEnemyView enemy in enemies)
            {
                enemy.Destroying -= enemyDestroyingActions[enemy];

                enemy.Destroy();
            }
            enemies.Clear();
            enemyDestroyingActions.Clear();
        }

        private void OnEnemyDestroying(IEnemyView enemy)
        {
            enemy.Destroying -= enemyDestroyingActions[enemy];

            enemies.Remove(enemy);
            enemyDestroyingActions.Remove(enemy);
        }
    }
}
