namespace Mgfirefox.CrisisTd
{
    public class BaseData : AbstractData
    {
        public class Builder
        {
            private readonly BaseData data = new();

            public BaseData Build()
            {
                return data;
            }

            public Builder FromConfiguration(BaseDataConfiguration configuration)
            {
                return WithMaxHealth(configuration.Health).WithHealth(configuration.Health);
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

        public HealthServiceData HealthServiceData { get; set; } = new();

        public static Builder CreateBuilder()
        {
            return new Builder();
        }
    }
}
