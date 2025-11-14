using System.Collections.Generic;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface IRayView : IView, IRayModel
    {
        new Vector3 StartPosition { get; set; }
        new Vector3 EndPosition { get; set; }
        new Vector3 Direction { get; set; }

        new float MaxDistance { get; set; }

        new LayerMask CollisionLayerMask { get; set; }

        void SetLine(Vector3 startPosition, Vector3 endPosition);
        void SetNormalizedLine(Vector3 startPosition, Vector3 endPosition);
        void SetRay(Vector3 startPosition, Vector3 direction);
        void SetRay(Vector3 startPosition, Vector3 direction, float maxDistance);
        void SetNormalizedRay(Vector3 startPosition, Vector3 direction);

        bool TryCast(out RayHit hit);
        IList<RayHit> CastAll();
    }

    public interface IRayView<TITargetView> : IRayView
        where TITargetView : class, IView
    {
        bool TryCastTarget(out RayHit<TITargetView> hit);
        IList<RayHit<TITargetView>> CastAllTargets();
    }
}
