using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    [CreateAssetMenu(fileName = "PlayerConfiguration", menuName = "Configuration/Player")]
    public class PlayerConfiguration : EntityConfiguration<PlayerPresenter, PlayerView,
        PlayerLifetimeScope, PlayerDataConfiguration>
    {
    }
}
