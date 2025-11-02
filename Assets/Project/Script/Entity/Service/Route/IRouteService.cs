using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface IRouteService : IService
    {
        Vector3 GetNextWaypoint(int index, Vector3 position, float epsilon);
        bool IsLastWaypoint(int index);

        Vector3 RestorePosition(int index, Vector3 position, float epsilon);
    }
}
