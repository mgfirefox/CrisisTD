using System.Globalization;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class AbstractAttackActionUi<TIView> : AbstractActionUi, IAttackActionUi<TIView>
        where TIView : class, IAttackActionView
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private TextMeshProUGUI damageText;
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private TextMeshProUGUI fireRateText;

        public virtual TIView View
        {
            set
            {
                damageText.text = value.Damage.ToString(CultureInfo.InvariantCulture);
                fireRateText.text = value.MaxCooldown.ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}
