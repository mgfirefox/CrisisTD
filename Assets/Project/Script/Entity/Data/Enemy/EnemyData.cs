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
                    .WithMaxHealth(configuration.Health).WithHealth(configuration.Health).WithShield(configuration.Shield).WithArmor(configuration.Armor);
            }
            
            public Builder WithId(EnemyId id)
            {
                data.Id = id;

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
                data.ArmoredHealthServiceData.MaxHealth = maxHealth;

                return this;
            }

            public Builder WithHealth(float health)
            {
                data.ArmoredHealthServiceData.Health = health;

                return this;
            }
            
            public Builder WithShield(float shield)
            {
                data.ArmoredHealthServiceData.Shield = shield;

                return this;
            }

            public Builder WithArmor(float armor)
            {
                data.ArmoredHealthServiceData.Armor = armor;

                return this;
            }
        }
        
        public EnemyId Id { get; set; } = EnemyId.Undefined;

        public EnemyTransformServiceData TransformServiceData { get; set; } = new();

        public ArmoredHealthServiceData ArmoredHealthServiceData { get; set; } = new();

        public static Builder CreateBuilder()
        {
            return new Builder();
        }
    }
}
