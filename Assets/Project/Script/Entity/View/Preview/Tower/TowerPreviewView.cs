using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class TowerPreviewView : AbstractView, ITowerPreviewView
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private RangeFolder rangeFolder;

        [SerializeField]
        [BoxGroup("Transform")]
        [ReadOnly]
        private Vector3 position;
        [SerializeField]
        [BoxGroup("Transform")]
        [ReadOnly]
        private Quaternion orientation;

        public IRangeFolder RangeFolder => rangeFolder;

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

                Transform.position = position;
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

                Transform.rotation = orientation;
            }
        }

        protected override IUnitySceneObject GetChildParent(IUnitySceneObject child)
        {
            if (child.Transform.TryGetComponent(out IRangeView _))
            {
                return rangeFolder;
            }

            return base.GetChildParent(child);
        }

        public void OnDrawGizmos()
        {
            if (IsHidden)
            {
                return;
            }

            if (!(Constant.towerObstacleLength > 0.0f && Constant.towerObstacleHeight > 0.0f &&
                  Constant.towerObstacleWidth > 0.0f))
            {
                return;
            }

            Color oldColor = Gizmos.color;
            Matrix4x4 oldMatrix = Gizmos.matrix;

            var scale = new Vector3(1.0f, Constant.epsilon, 1.0f);

            Gizmos.color = Color.red;
            Gizmos.matrix = Matrix4x4.TRS(position, orientation, scale);

            Gizmos.DrawCube(Vector3.zero,
                new Vector3(Constant.towerObstacleLength, Constant.towerObstacleHeight,
                    Constant.towerObstacleWidth));

            Gizmos.color = oldColor;
            Gizmos.matrix = oldMatrix;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            position = Transform.position;
            orientation = Transform.rotation;

            rangeFolder.Initialize();
        }

        protected override void OnDestroying()
        {
            rangeFolder.Destroy();

            base.OnDestroying();
        }

        protected override void OnShowing()
        {
            foreach (IRangeView range in rangeFolder.Children)
            {
                range.Show();
            }

            base.OnShowing();
        }

        protected override void OnHiding()
        {
            foreach (IRangeView range in rangeFolder.Children)
            {
                range.Hide();
            }

            base.OnHiding();
        }
    }
}
