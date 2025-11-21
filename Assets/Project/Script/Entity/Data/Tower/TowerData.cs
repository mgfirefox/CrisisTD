using System.Collections.Generic;
using Mgfirefox.CrisisTd.Level;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class TowerData : AbstractData
    {
        public class Builder
        {
            private readonly ITowerActionDataFactory actionDataFactory;

            private readonly TowerData data = new();

            public Builder()
            {
                IAttackActionDataFactory attackActionDataFactory = new AttackActionDataFactory();
                IBuffActionDataFactory buffActionDataFactory = new BuffActionDataFactory();
                actionDataFactory =
                    new TowerActionDataFactory(attackActionDataFactory, buffActionDataFactory);
            }

            public TowerData Build()
            {
                return data;
            }

            public Builder FromConfiguration(TowerDataConfiguration configuration)
            {
                return WithPriority(configuration.Priority)
                    .FromLevelDataConfigurations(configuration.Type,
                        configuration.LevelDataConfigurations);
            }

            public Builder FromLevelDataConfigurations(TowerType type,
                IDictionary<BranchLevel, LevelDataConfiguration> levelDataConfigurations)
            {
                data.Type = type;

                foreach ((BranchLevel level, LevelDataConfiguration levelDataConfiguration) in
                         levelDataConfigurations)
                {
                    var levelData = new LevelItem
                    {
                        Level = level,
                    };
                    
                    foreach (AbstractTowerActionDataConfiguration actionDataConfiguration in
                             levelDataConfiguration.TowerActionDataConfigurations)
                    {
                        if (actionDataFactory.TryCreate(type, actionDataConfiguration,
                                data.Priority, out AbstractTowerActionData actionData))
                        {
                            levelData.ActionDataList.Add(actionData);
                        }
                        else
                        {
                            // TODO: Change Warning
                            Debug.LogWarning(
                                $"Failed to create Object of type {typeof(AbstractTowerActionData)} with ID \"${type}\"");
                        }
                    }
                    
                    data.LevelServiceData.Items[level] = levelData;
                }

                return this;
            }

            public Builder WithId(TowerId id)
            {
                data.Id = id;

                return this;
            }

            public Builder WithPriority(TargetPriority priority)
            {
                data.Priority = priority;

                return this;
            }

            public Builder WithPosition(Vector3 position)
            {
                data.TransformServiceData.Position = position;

                return this;
            }

            public Builder WithOrientation(Quaternion orientation)
            {
                data.TransformServiceData.Orientation = orientation;

                return this;
            }

            public Builder WithLevel(BranchLevel level)
            {
                data.LevelServiceData.Level = level;

                return this;
            }

            public Builder WithModels(IDictionary<BranchLevel, IModelComponent> models)
            {
                data.ModelServiceData.Models = models;
                
                return this;
            }
        }

        public TowerId Id { get; set; } = TowerId.Undefined;

        public TowerType Type { get; set; } = TowerType.Undefined;

        public TargetPriority Priority { get; set; } = TargetPriority.Undefined;

        public TowerTransformServiceData TransformServiceData { get; set; } = new();

        public TowerAllEffectServiceData AllEffectServiceData { get; set; } = new();

        public LevelServiceData LevelServiceData { get; set; } = new();
        
        public TowerModelServiceData ModelServiceData { get; set; } = new();
        public TowerAnimationServiceData AnimationServiceData { get; set; } = new();

        public static Builder CreateBuilder()
        {
            return new Builder();
        }
    }
}
