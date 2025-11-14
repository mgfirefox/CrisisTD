using System.Globalization;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class ArcAngleAttackActionUi : AbstractAttackActionUi<IArcAngleAttackActionView>,
        IArcAngleAttackActionUi
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private TextMeshProUGUI arcAngleText;
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private TextMeshProUGUI pelletCountText;
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private TextMeshProUGUI maxPelletHitCountText;

        public override IArcAngleAttackActionView View
        {
            set
            {
                base.View = value;

                arcAngleText.text = value.ArcAngle.ToString(CultureInfo.InvariantCulture);
                pelletCountText.text = value.MaxPelletCount.ToString();
                maxPelletHitCountText.text = value.MaxPelletHitCount.ToString();
            }
        }
    }
}
