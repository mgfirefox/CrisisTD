using System;
using System.Globalization;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class HealthService : AbstractDataService<HealthServiceData>, IHealthService
    {
        public float MaxHealth { get; private set; }
        public float Health { get; private set; }

        public bool IsDied { get; private set; }

        private bool ShouldDie
        {
            get
            {
                float epsilon = Scene.Settings.MathSettings.Epsilon;

                if (Health.IsApproximateNonPositive(epsilon))
                {
                    return true;
                }

                return false;
            }
        }

        public event Action Died;

        [Inject]
        public HealthService(Scene scene) : base(scene)
        {
        }

        public void TakeDamage(float damage)
        {
            if (IsDied)
            {
                return;
            }

            float epsilon = Scene.Settings.MathSettings.Epsilon;

            if (damage.IsApproximateNonPositive(epsilon))
            {
                throw new InvalidArgumentException(nameof(damage),
                    damage.ToString(CultureInfo.InvariantCulture));
            }
            if (damage.IsApproximateZero(epsilon))
            {
                return;
            }

            Health -= damage;
            if (!ShouldDie)
            {
                return;
            }

            Die();
        }

        public void Heal(float health)
        {
            if (IsDied)
            {
                return;
            }

            float epsilon = Scene.Settings.MathSettings.Epsilon;

            if (health.IsApproximateNonPositive(epsilon))
            {
                throw new InvalidArgumentException(nameof(health),
                    health.ToString(CultureInfo.InvariantCulture));
            }
            if (health.IsApproximateZero(epsilon))
            {
                return;
            }

            Health += health;
            if (Health.IsLessThanApproximately(MaxHealth, epsilon))
            {
                return;
            }

            Health = MaxHealth;
        }

        public void Die()
        {
            if (IsDied)
            {
                return;
            }

            Health = 0.0f;

            IsDied = true;

            Died?.Invoke();
        }

        protected override void OnInitialized(HealthServiceData data)
        {
            base.OnInitialized(data);

            MaxHealth = data.MaxHealth;
            Health = data.Health;
            IsDied = ShouldDie;

            if (IsDied)
            {
                Die();
            }
        }

        protected override void OnDestroying()
        {
            Die();

            base.OnDestroying();
        }
    }
}
