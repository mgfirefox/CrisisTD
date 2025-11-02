using System;
using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class EnemyView : AbstractVisualView, IEnemyView
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

        public bool IsDied { get => isDied; set => isDied = value; }

        public event Action<float> DamageTaken;
        public event Action Died;

        public void TakeDamage(float damage)
        {
            DamageTaken?.Invoke(damage);
        }

        public void Die()
        {
            Died?.Invoke();
        }
    }
}
