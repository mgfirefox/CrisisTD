using System.Globalization;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractTowerActionUi<TIView> : AbstractActionUi, ITowerActionUi<TIView>
        where TIView : class, ITowerActionView
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private TextMeshProUGUI rangeText;

        public virtual TIView View
        {
            set => rangeText.text = value.RangeRadius.ToString(CultureInfo.InvariantCulture);
        }
    }
}
