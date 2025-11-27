using System;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface IEnemyView : IView, IEnemyModel
    {
        new Vector3 Position { get; set; }
        new Quaternion Orientation { get; set; }

        Vector3 InitialPosition { set; }
        Quaternion InitialOrientation { set; }

        new float MaxMovementSpeed { get; set; }
        new float MovementSpeed { get; set; }

        new int WaypointIndex { get; set; }

        new float MaxHealth { get; set; }
        new float Health { get; set; }
        
        new float Shield { get; set; }
        new float Armor { get; set; }

        new bool IsDied { get; set; }

        event Action<float, float> DamageTaken;
        event Action Died;

        void TakeDamage(float damage, float armorPiercing);
        void Die();
    }
}
