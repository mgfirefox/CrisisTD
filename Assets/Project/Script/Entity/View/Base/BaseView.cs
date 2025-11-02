using System;
using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class BaseView : AbstractView, IBaseView
    {
        [SerializeField]
        [BoxGroup("Health")]
        [ReadOnly]
        private float maxHealth;
        [SerializeField]
        [BoxGroup("Health")]
        [ReadOnly]
        private float health;

        [SerializeField]
        [BoxGroup("Health")]
        [ReadOnly]
        public bool isDied;

        public float MaxHealth { get => maxHealth; set => maxHealth = value; }
        public float Health { get => health; set => health = value; }

        public bool IsDied { get => isDied; set => isDied = value; }

        public event Action<IEnemyView> DamageTaken;
        public event Action Died;

        public void TakeDamage(IEnemyView enemy)
        {
            DamageTaken?.Invoke(enemy);
        }

        public void Die()
        {
            Died?.Invoke();
        }
    }
}
