using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Mgfirefox.CrisisTd
{
    public class PlayerFactory : AbstractFactory, IPlayerFactory
    {
        private readonly PlayerConfiguration configuration;

        [Inject]
        public PlayerFactory(PlayerConfiguration configuration, LifetimeScope parentLifetimeScope) :
            base(parentLifetimeScope)
        {
            this.configuration = configuration;
        }

        public IPlayerView Create(Vector3 position, Quaternion orientation)
        {
            PlayerLifetimeScope lifetimeScope =
                ParentLifetimeScope.CreateChildFromPrefabRespectSettings(configuration
                    .LifetimeScopePrefab);
            IObjectResolver lifetimeScopeContainer = lifetimeScope.Container;

            var view = lifetimeScopeContainer.Resolve<IPlayerView>();

            PlayerData data = PlayerData.CreateBuilder()
                .FromConfiguration(configuration.DataConfiguration).WithPosition(position)
                .WithOrientation(orientation).Build();

            var presenter = lifetimeScopeContainer.Resolve<IPlayerPresenter>();
            presenter.Initialize(data);

            return view;
        }

        public bool TryCreate(Vector3 position, Quaternion orientation, out IPlayerView view)
        {
            try
            {
                view = Create(position, orientation);

                return true;
            }
            catch (Exception e)
            {
                if (e is not (InvalidArgumentException or ConfigurationNotFoundException))
                {
                    Debug.LogException(new CaughtUnexpectedException(e));
                }
            }

            view = null;

            return false;
        }
    }
}
