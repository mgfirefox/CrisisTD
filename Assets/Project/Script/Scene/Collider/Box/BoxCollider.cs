using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class BoxCollider : AbstractCollider<UnityEngine.BoxCollider>, IBoxCollider
    {
        [SerializeField]
        [BoxGroup("BoxCollider")]
        [ReadOnly]
        private Vector3 size;

        public Vector3 Size
        {
            get => size;
            set
            {
                size = value;

                if (IsDestroyed)
                {
                    return;
                }

                Collider.size = size;
            }
        }
        public float Length { get => size.x; set => SetSize(value, size.y, size.z); }
        public float Height { get => size.y; set => SetSize(size.x, value, size.z); }
        public float Width { get => size.z; set => SetSize(size.x, size.y, value); }

        public void SetSize(float length, float height, float width)
        {
            Size = new Vector3(length, height, width);
        }
    }
}
