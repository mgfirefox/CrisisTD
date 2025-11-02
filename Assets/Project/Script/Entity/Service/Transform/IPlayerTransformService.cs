using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface IPlayerTransformService : IMovingTransformService<PlayerTransformServiceData>,
        IPlayerTransformModel
    {
        void Move(Vector2 translationDirection);
    }
}
