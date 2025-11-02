using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class EnemyData : AbstractData
    {
        public class Builder
        {
            private readonly EnemyData data = new();

            public EnemyData Build()
            {
                return data;
            }

            public Builder FromConfiguration(EnemyDataConfiguration configuration)
            {
                return WithMaxMovementSpeed(configuration.MovementSpeed)
                    .WithMaxHealth(configuration.Health).WithHealth(configuration.Health);
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

            public Builder WithMaxHealth(float maxHealth)
            {
                data.HealthServiceData.MaxHealth = maxHealth;

                return this;
            }

            public Builder WithHealth(float health)
            {
                data.HealthServiceData.Health = health;

                return this;
            }
        }

        public EnemyTransformServiceData TransformServiceData { get; set; } = new();

        public HealthServiceData HealthServiceData { get; set; } = new();

        public static Builder CreateBuilder()
        {
            return new Builder();
        }
    }
}
