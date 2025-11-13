using System.Globalization;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractBuffActionUi<TIView> : AbstractActionUi, IBuffActionUi<TIView>
        where TIView : class, IBuffActionView
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private TextMeshProUGUI typeText;
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private TextMeshProUGUI valueText;

        public virtual TIView View
        {
            set
            {
                typeText.text = value.Type.ToString();
                valueText.text = value.Value.ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}
