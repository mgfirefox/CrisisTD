using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Mgfirefox.CrisisTd
{
    public class MapViewFactory : AbstractFactory, IMapViewFactory
    {
        private readonly IReadOnlyDictionary<MapId, MapView> viewPrefabs;

        [Inject]
        public MapViewFactory(IReadOnlyDictionary<MapId, MapView> viewPrefabs,
            LifetimeScope parentLifetimeScope) : base(parentLifetimeScope)
        {
            this.viewPrefabs = viewPrefabs;
        }

        public IMapView Create(MapId id)
        {
            if (!MapIdValidator.TryValidate(id))
            {
                throw new InvalidArgumentException(nameof(id), id.ToString());
            }

            if (viewPrefabs.TryGetValue(id, out MapView viewPrefab))
            {
                IMapView view = ParentLifetimeScope.Container.Instantiate(viewPrefab);
                view.Initialize();

                return view;
            }

            throw new PrefabNotFoundException(id.ToString(), typeof(MapView).ToString());
        }

        public bool TryCreate(MapId id, out IMapView view)
        {
            try
            {
                view = Create(id);

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
