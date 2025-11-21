using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface ITransformService<in TData> : IDataService<TData>, ITransformModel
        where TData : TransformServiceData
    {
        Vector3 PivotPoint { get; set; }
        Vector3 PivotPointPosition { get; }
    }
}
