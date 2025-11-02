using System;

namespace Mgfirefox.CrisisTd
{
    public interface IHealthService : IDataService<HealthServiceData>, IHealthModel
    {
        event Action Died;

        void TakeDamage(float damage);
        void Heal(float health);
        void Die();
    }
}
