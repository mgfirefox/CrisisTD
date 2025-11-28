using System.Globalization;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class EconomyServiceUi : AbstractServiceUi, IEconomyServiceUi
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private TextMeshProUGUI moneyText;

        public int Money
        {
            set => moneyText.text = value.ToString();
        }
    }
}
