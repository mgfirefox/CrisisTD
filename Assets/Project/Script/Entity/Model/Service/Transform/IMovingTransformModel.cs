namespace Mgfirefox.CrisisTd
{
    public interface IMovingTransformModel : ITransformModel
    {
        public float MaxMovementSpeed { get; }
        public float MovementSpeed { get; }
    }
}
