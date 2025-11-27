using System;
using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class EnemyView : AbstractView, IEnemyView
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private new Rigidbody rigidbody;

        [SerializeField]
        [BoxGroup("Transform")]
        [ReadOnly]
        private Vector3 position;
        [SerializeField]
        [BoxGroup("Transform")]
        [ReadOnly]
        private Quaternion orientation;

        [SerializeField]
        [BoxGroup("Transform")]
        [ReadOnly]
        private float maxMovementSpeed;
        [SerializeField]
        [BoxGroup("Transform")]
        [ReadOnly]
        private float movementSpeed;

        [SerializeField]
        [BoxGroup("Transform")]
        [ReadOnly]
        private int waypointIndex;

        [SerializeField]
        [BoxGroup("Health")]
        [ReadOnly]
        private float maxHealth;
        [SerializeField]
        [BoxGroup("Health")]
        [ReadOnly]
        private float health;
        
        [SerializeField]
        [BoxGroup("ArmoredHealth")]
        [ReadOnly]
        private float shield;
        [SerializeField]
        [BoxGroup("ArmoredHealth")]
        [ReadOnly]
        private float armor;

        [SerializeField]
        [BoxGroup("Health")]
        [ReadOnly]
        public bool isDied;

        public Vector3 Position
        {
            get => position;
            set
            {
                position = value;

                if (IsDestroyed)
                {
                    return;
                }

                rigidbody.MovePosition(position);
            }
        }
        public Quaternion Orientation
        {
            get => orientation;
            set
            {
                orientation = value;

                if (IsDestroyed)
                {
                    return;
                }

                rigidbody.MoveRotation(orientation);
            }
        }

        public Vector3 InitialPosition
        {
            set
            {
                position = value;

                if (IsDestroyed)
                {
                    return;
                }

                rigidbody.position = position;
            }
        }
        public Quaternion InitialOrientation
        {
            set
            {
                orientation = value;

                if (IsDestroyed)
                {
                    return;
                }

                rigidbody.rotation = orientation;
            }
        }

        public float MaxMovementSpeed { get => maxMovementSpeed; set => maxMovementSpeed = value; }
        public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }

        public int WaypointIndex { get => waypointIndex; set => waypointIndex = value; }

        public float MaxHealth { get => maxHealth; set => maxHealth = value; }
        public float Health { get => health; set => health = value; }
        
        public float Shield { get => shield; set => shield = value; }
        public float Armor { get => armor; set => armor = value; }

        public bool IsDied { get => isDied; set => isDied = value; }

        public event Action<float, float> DamageTaken;
        public event Action Died;

        public void TakeDamage(float damage, float armorPiercing)
        {
            DamageTaken?.Invoke(damage, armorPiercing);
        }

        public void Die()
        {
            Died?.Invoke();
        }
    }
}
