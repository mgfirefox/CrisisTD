using System;
using NaughtyAttributes;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Mgfirefox.CrisisTd
{
    public class GameSceneLifetimeScope : LifetimeScope
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private SceneSettings sceneSettings;

        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private CameraView cameraView;
        
        [SerializeField]
        [BoxGroup("Test Dependencies")]
        [Required]
        private Tracer tracerPrefab;
        [SerializeField]
        [BoxGroup("Test Dependencies")]
        [Required]
        private Explosion explosionPrefab;
        
        [SerializeField]
        [BoxGroup("Ui")]
        [Required]
        private BaseUi baseUi;
        [SerializeField]
        [BoxGroup("Ui")]
        [Required]
        private TowerPlacementActionUi towerPlacementActionUi;
        [SerializeField]
        [BoxGroup("Ui")]
        [Required]
        private TowerInteractionActionUi towerInteractionActionUi;

        [SerializeField]
        [BoxGroup("Views")]
        private RangeViewDictionary rangeViewPrefabs = new();
        [SerializeField]
        [BoxGroup("Views")]
        private TowerObstacleView towerObstacleViewPrefab;

        [SerializeField]
        [BoxGroup("Actions")]
        private AttackActionLifetimeScopeDictionary attackActionLifetimeScopePrefabs = new();
        [SerializeField]
        [BoxGroup("Actions")]
        private BuffActionLifetimeScopeDictionary buffActionLifetimeScopePrefabs = new();
        [SerializeField]
        [BoxGroup("Actions")]
        [Required]
        private TowerPlacementActionLifetimeScope towerPlacementActionLifetimeScopePrefab;
        [SerializeField]
        [BoxGroup("Actions")]
        [Required]
        private TowerInteractionActionLifetimeScope towerInteractionActionLifetimeScopePrefab;

        [SerializeField]
        [BoxGroup("Player")]
        [Required]
        private PlayerConfiguration playerConfiguration;

        [SerializeField]
        [BoxGroup("Map")]
        private MapViewDictionary mapViewPrefabs = new();

        [SerializeField]
        [BoxGroup("Enemy")]
        [Required]
        private EnemyConfiguration enemyConfiguration;

        [SerializeField]
        [BoxGroup("Base")]
        [Required]
        private BaseConfiguration baseConfiguration;

        [SerializeField]
        [BoxGroup("Tower")]
        private TowerActionLifetimeScopeDictionary towerActionLifetimeScopePrefabs = new();
        [BoxGroup("Tower")]
        [SerializeField]
        [Required]
        private TowerPreviewView towerPreviewViewPrefab;
        [SerializeField]
        [BoxGroup("Tower")]
        private TowerConfigurationDictionary towerConfigurations = new();

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterEntryPoint<GameSceneEntryPoint>();

            RegisterScene(builder);

            RegisterUi(builder);

            RegisterTest(builder);

            RegisterServices(builder);
            RegisterViews(builder);
            RegisterActions(builder);

            RegisterPlayer(builder);

            RegisterMap(builder);

            RegisterEnemy(builder);

            RegisterBase(builder);

            RegisterTower(builder);
        }

        private void RegisterScene(IContainerBuilder builder)
        {
            builder.RegisterComponent(cameraView).AsImplementedInterfaces();

            builder.RegisterInstance(sceneSettings);

            builder.Register<Scene>(Lifetime.Singleton);
            builder.Register<SceneLoop>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }

        private void RegisterUi(IContainerBuilder builder)
        {
            builder.RegisterComponent(baseUi).AsImplementedInterfaces();
            builder.RegisterComponent(towerPlacementActionUi).AsImplementedInterfaces();
            builder.RegisterComponent(towerInteractionActionUi).AsImplementedInterfaces();
        }

        private void RegisterTest(IContainerBuilder builder)
        {
            builder.RegisterInstance(tracerPrefab);
            builder.RegisterInstance(explosionPrefab);
        }

        private void RegisterServices(IContainerBuilder builder)
        {
            builder.Register<TranslationService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<RotationService>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<RouteService>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<TimeService>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<CameraService>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void RegisterViews(IContainerBuilder builder)
        {
            RegisterRangeViewPrefabs(builder);
            builder.RegisterInstance(towerObstacleViewPrefab);
        }

        private void RegisterRangeViewPrefabs(IContainerBuilder builder)
        {
            foreach (RangeType type in Enum.GetValues(typeof(RangeType)))
            {
                string typeString = $"{typeof(AbstractRangeView)} Prefab";

                if (RangeTypeValidator.TryValidate(type))
                {
                    if (!rangeViewPrefabs.ContainsKey(type))
                    {
                        throw new MissingRegistrationException(type.ToString(), typeString);
                    }
                }
                else
                {
                    if (rangeViewPrefabs.Remove(type))
                    {
                        Debug.LogWarning(
                            Warning.RedundantRegistrationIgnoredMessage(type.ToString(),
                                typeString), this);
                    }
                }
            }

            builder.RegisterDictionaryAsReadOnly(rangeViewPrefabs);
        }

        public void RegisterActions(IContainerBuilder builder)
        {
            RegisterAttackActionLifetimeScopePrefabs(builder);
            RegisterBuffActionLifetimeScopePrefabs(builder);
            builder.RegisterInstance(towerPlacementActionLifetimeScopePrefab);
            builder.RegisterInstance(towerInteractionActionLifetimeScopePrefab);
        }

        private void RegisterAttackActionLifetimeScopePrefabs(IContainerBuilder builder)
        {
            foreach (AttackActionType type in Enum.GetValues(typeof(AttackActionType)))
            {
                string typeString = $"{typeof(AbstractAttackActionLifetimeScope)} Prefab";

                if (AttackActionTypeValidator.TryValidate(type))
                {
                    if (!attackActionLifetimeScopePrefabs.ContainsKey(type))
                    {
                        throw new MissingRegistrationException(type.ToString(), typeString);
                    }
                }
                else
                {
                    if (attackActionLifetimeScopePrefabs.Remove(type))
                    {
                        Debug.LogWarning(
                            Warning.RedundantRegistrationIgnoredMessage(type.ToString(),
                                typeString), this);
                    }
                }
            }

            builder.RegisterDictionaryAsReadOnly(attackActionLifetimeScopePrefabs);
        }

        private void RegisterBuffActionLifetimeScopePrefabs(IContainerBuilder builder)
        {
            foreach (BuffActionType type in Enum.GetValues(typeof(BuffActionType)))
            {
                string typeString = $"{typeof(AbstractBuffActionLifetimeScope)} Prefab";

                if (BuffActionTypeValidator.TryValidate(type))
                {
                    if (!buffActionLifetimeScopePrefabs.ContainsKey(type))
                    {
                        throw new MissingRegistrationException(type.ToString(), typeString);
                    }
                }
                else
                {
                    if (buffActionLifetimeScopePrefabs.Remove(type))
                    {
                        Debug.LogWarning(
                            Warning.RedundantRegistrationIgnoredMessage(type.ToString(),
                                typeString), this);
                    }
                }
            }

            builder.RegisterDictionaryAsReadOnly(buffActionLifetimeScopePrefabs);
        }

        private void RegisterPlayer(IContainerBuilder builder)
        {
            builder.RegisterInstance(playerConfiguration);

            builder.Register<PlayerService>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<PlayerFactory>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void RegisterMap(IContainerBuilder builder)
        {
            RegisterMapViewPrefabs(builder);

            builder.Register<MapService>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<MapViewFactory>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void RegisterMapViewPrefabs(IContainerBuilder builder)
        {
            foreach (MapId id in Enum.GetValues(typeof(MapId)))
            {
                string type = $"{typeof(MapView)} Prefab";

                if (MapIdValidator.TryValidate(id))
                {
                    if (!mapViewPrefabs.ContainsKey(id))
                    {
                        throw new MissingRegistrationException(id.ToString(), type);
                    }
                }
                else
                {
                    if (mapViewPrefabs.Remove(id))
                    {
                        Debug.LogWarning(
                            Warning.RedundantRegistrationIgnoredMessage(id.ToString(), type), this);
                    }
                }
            }

            builder.RegisterDictionaryAsReadOnly(mapViewPrefabs);
        }

        private void RegisterEnemy(IContainerBuilder builder)
        {
            builder.RegisterInstance(enemyConfiguration);

            builder.Register<EnemyService>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<EnemyFactory>(Lifetime.Singleton).AsImplementedInterfaces();

            // TODO: Remove it
            builder.Register<EnemyTest>(Lifetime.Singleton);
        }

        private void RegisterBase(IContainerBuilder builder)
        {
            builder.RegisterInstance(baseConfiguration);

            builder.Register<BaseService>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<BaseFactory>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void RegisterTower(IContainerBuilder builder)
        {
            RegisterTowerActionLifetimeScopePrefabs(builder);

            builder.RegisterInstance(towerPreviewViewPrefab);
            RegisterTowerConfigurations(builder);

            builder.Register<TowerService>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<TowerFactory>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void RegisterTowerActionLifetimeScopePrefabs(IContainerBuilder builder)
        {
            foreach (TowerType type in Enum.GetValues(typeof(TowerType)))
            {
                string typeString = $"{typeof(AbstractTowerActionLifetimeScope)} Prefab";

                if (TowerTypeValidator.TryValidate(type))
                {
                    if (!towerActionLifetimeScopePrefabs.ContainsKey(type))
                    {
                        throw new MissingRegistrationException(type.ToString(), typeString);
                    }
                }
                else
                {
                    if (towerActionLifetimeScopePrefabs.Remove(type))
                    {
                        Debug.LogWarning(
                            Warning.RedundantRegistrationIgnoredMessage(type.ToString(),
                                typeString), this);
                    }
                }
            }

            builder.RegisterDictionaryAsReadOnly(towerActionLifetimeScopePrefabs);
        }

        private void RegisterTowerConfigurations(IContainerBuilder builder)
        {
            foreach (TowerId id in Enum.GetValues(typeof(TowerId)))
            {
                string type = $"{typeof(TowerConfiguration)} Prefab";

                if (TowerIdValidator.TryValidate(id))
                {
                    if (!towerConfigurations.ContainsKey(id))
                    {
                        throw new MissingRegistrationException(id.ToString(), type);
                    }
                }
                else
                {
                    if (towerConfigurations.Remove(id))
                    {
                        Debug.LogWarning(
                            Warning.RedundantRegistrationIgnoredMessage(id.ToString(), type), this);
                    }
                }
            }

            builder.RegisterDictionaryAsReadOnly(towerConfigurations);
        }
    }
}
