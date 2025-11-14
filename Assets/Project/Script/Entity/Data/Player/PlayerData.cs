using System.Collections.Generic;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class PlayerData : AbstractData
    {
        public class Builder
        {
            private readonly PlayerData data = new();

            public PlayerData Build()
            {
                return data;
            }

            public Builder FromConfiguration(PlayerDataConfiguration configuration)
            {
                data.LoadoutServiceData.Items = new List<LoadoutItem>(configuration.TowerLoadout);

                return WithMaxMovementSpeed(configuration.MovementSpeed)
                    .WithLimit(configuration.Limit);
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

            public Builder WithMaxMovementSpeed(float maxMovementSpeed)
            {
                data.TransformServiceData.MaxMovementSpeed = maxMovementSpeed;

                return this;
            }

            public Builder WithMovementSpeed(float movementSpeed)
            {
                data.TransformServiceData.MovementSpeed = movementSpeed;

                return this;
            }

            public Builder WithSelectedIndex(int selectedIndex)
            {
                data.TowerPlacementActionData.SelectedIndex = selectedIndex;

                return this;
            }

            public Builder WithLimit(int limit)
            {
                data.TowerPlacementActionData.Limit = limit;

                return this;
            }

            public Builder WithCount(int count)
            {
                data.TowerPlacementActionData.Count = count;

                return this;
            }
        }

        public PlayerTransformServiceData TransformServiceData { get; set; } = new();

        public LoadoutServiceData LoadoutServiceData { get; set; } = new();
        public TowerPlacementActionData TowerPlacementActionData { get; set; } = new();

        public TowerInteractionActionData TowerInteractionActionData { get; set; } = new();

        public static Builder CreateBuilder()
        {
            return new Builder();
        }
    }
}
