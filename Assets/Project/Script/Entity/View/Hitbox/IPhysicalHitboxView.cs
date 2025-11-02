using System;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface IPhysicalHitboxView : IHitboxView
    {
        Vector3 GetClosestPosition(Vector3 position);

        bool IsPositionWithin(Vector3 position, float epsilon);
    }

    public interface IPhysicalHitboxView<out TITargetView> : IPhysicalHitboxView
        where TITargetView : class, IView
    {
        event Action<TITargetView> TargetEntered;
        event Action<TITargetView> TargetExited;
    }
}
