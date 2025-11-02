namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractMovingTransformService<TData> : AbstractTransformService<TData>,
        IMovingTransformService<TData>
        where TData : MovingTransformServiceData
    {
        public float MaxMovementSpeed { get; protected set; }
        public float MovementSpeed { get; protected set; }

        protected AbstractMovingTransformService(Scene scene) : base(scene)
        {
        }

        protected override void OnInitialized(TData data)
        {
            base.OnInitialized(data);

            MaxMovementSpeed = data.MaxMovementSpeed;
            MovementSpeed = data.MovementSpeed;
        }
    }
}
