using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    [CreateAssetMenu(fileName = "BaseConfiguration", menuName = "Configuration/Base")]
    public class BaseConfiguration : EntityConfiguration<BasePresenter, BaseView, BaseLifetimeScope,
        BaseDataConfiguration>
    {
    }
}
