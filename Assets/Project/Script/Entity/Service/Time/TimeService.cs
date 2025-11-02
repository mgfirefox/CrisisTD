using UnityEngine;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class TimeService : AbstractService, ITimeService
    {
        public float DeltaTime => Time.deltaTime;
        public float FixedDeltaTime => Time.fixedDeltaTime;

        [Inject]
        public TimeService()
        {
        }
    }
}
