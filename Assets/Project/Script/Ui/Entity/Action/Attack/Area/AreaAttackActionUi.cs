using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class AreaAttackActionUi : AbstractAttackActionUi<IAreaAttackActionView>,
        IAreaAttackActionUi
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private TextMeshProUGUI maxHitCountText;

        public override IAreaAttackActionView View
        {
            set
            {
                base.View = value;

                maxHitCountText.text = value.MaxHitCount.ToString();
            }
        }
    }
}
