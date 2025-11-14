using System;

namespace Mgfirefox.CrisisTd
{
    public interface IBaseView : IView, IBaseModel
    {
        new float MaxHealth { get; set; }
        new float Health { get; set; }

        new bool IsDied { get; set; }

        event Action<IEnemyView> DamageTaken;
        event Action Died;

        void TakeDamage(IEnemyView enemy);
        void Die();
    }
}
