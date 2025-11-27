using System;

namespace Mgfirefox.CrisisTd
{
    public interface IArmoredHealthService : IDataService<ArmoredHealthServiceData>, IArmoredHealthModel
    {
        event Action Died;
        
        void TakeDamage(float damage, float armorPiercing);
        void Die();
    }
}
