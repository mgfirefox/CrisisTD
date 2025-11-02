namespace Mgfirefox.CrisisTd
{
    public class MovingTransformServiceData : TransformServiceData, IMovingTransformModel
    {
        public float MaxMovementSpeed { get; set; }
        public float MovementSpeed { get; set; }
    }
}
