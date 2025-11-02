using System;

namespace Mgfirefox.CrisisTd
{
    public interface IEnemyTransformService : IMovingTransformService<EnemyTransformServiceData>,
        IEnemyTransformModel
    {
        event Action BaseReached;

        void Move();
    }
}
