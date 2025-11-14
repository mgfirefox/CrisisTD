using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class BurstAttackActionUi : AbstractAttackActionUi<IBurstAttackActionView>,
        IBurstAttackActionUi
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private TextMeshProUGUI burstShotCountText;

        public override IBurstAttackActionView View
        {
            set
            {
                base.View = value;

                burstShotCountText.text = value.BurstShotCount.ToString();
            }
        }
    }
}
