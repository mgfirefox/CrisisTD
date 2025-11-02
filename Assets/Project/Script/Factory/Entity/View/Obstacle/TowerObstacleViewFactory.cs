using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Mgfirefox.CrisisTd
{
    public class TowerObstacleViewFactory : AbstractFactory, ITowerObstacleViewFactory
    {
        private readonly TowerObstacleView viewPrefab;

        [Inject]
        public TowerObstacleViewFactory(TowerObstacleView viewPrefab,
            LifetimeScope parentLifetimeScope) : base(parentLifetimeScope)
        {
            this.viewPrefab = viewPrefab;
        }

        public ITowerObstacleView Create()
        {
            ITowerObstacleView view = ParentLifetimeScope.Container.Instantiate(viewPrefab);
            view.Initialize();

            return view;
        }

        public bool TryCreate(out ITowerObstacleView view)
        {
            try
            {
                view = Create();

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
