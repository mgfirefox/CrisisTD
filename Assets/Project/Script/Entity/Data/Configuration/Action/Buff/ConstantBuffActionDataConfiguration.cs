using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    [CreateAssetMenu(fileName = "BuffActionDataConfiguration",
        menuName = "DataConfiguration/Action/Buff/Constant")]
    public class ConstantBuffActionDataConfiguration : AbstractBuffActionDataConfiguration
    {
        public override BuffActionType Type => BuffActionType.Constant;
    }
}
