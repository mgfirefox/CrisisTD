using System;
using System.Globalization;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class ArmoredHealthService : AbstractDataService<ArmoredHealthServiceData>, IArmoredHealthService
    {
        public float MaxHealth { get; private set; }
        public float Health { get; private set; }
        
        public float Shield { get; private set; }
        public float Armor { get; private set; }

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
        public ArmoredHealthService(Scene scene) : base(scene)
        {
        }

        public void TakeDamage(float damage, float armorPiercing)
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

            if (Shield.IsApproximateZero(epsilon) || Armor.IsLessThanOrEqualApproximately(armorPiercing, epsilon))
            {
                Health -= damage;
                
                if (!ShouldDie)
                {
                    return;
                }

                Die();
            }
            else
            {
                Shield -= damage;

                if (Shield.IsApproximateNonPositive(epsilon))
                {
                    Shield = 0.0f;
                }
            }
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

        protected override void OnInitialized(ArmoredHealthServiceData data)
        {
            base.OnInitialized(data);

            MaxHealth = data.MaxHealth;
            Health = data.Health;
            Shield = data.Shield;
            Armor = data.Armor;
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
