using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractTransformService<TData> : AbstractDataService<TData>,
        ITransformService<TData>
        where TData : TransformServiceData
    {
        protected float Yaw { get; set; }
        
        public Vector3 PivotPoint { get; set; }
        public Vector3 PivotPointPosition => Position + PivotPoint;

        public Vector3 Position { get; protected set; }
        public virtual Quaternion Orientation => Quaternion.Euler(0.0f, Yaw, 0.0f);

        protected AbstractTransformService(Scene scene) : base(scene)
        {
        }

        protected override void OnInitialized(TData data)
        {
            base.OnInitialized(data);

            Vector3 orientationEulerAngles = data.Orientation.eulerAngles;

            Position = data.Position;
            Yaw = orientationEulerAngles.y;
        }
    }
}
