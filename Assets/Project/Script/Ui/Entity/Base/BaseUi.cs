using System.Globalization;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mgfirefox.CrisisTd
{
    public class BaseUi : AbstractUi, IBaseUi
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private Image image;
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private TextMeshProUGUI healthText;
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private TextMeshProUGUI maxHealthText;

        public void SetHealth(float health, float maxHealth)
        {
            if (IsDestroyed)
            {
                return;
            }

            float healthRatio;
            if (maxHealth > 0.0f)
            {
                healthRatio = health / maxHealth;
            }
            else
            {
                healthRatio = 0.0f;
            }

            float imageSize = Panel.rectTransform.rect.width * healthRatio;

            image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, imageSize);

            healthText.text = Mathf.CeilToInt(health).ToString(CultureInfo.InvariantCulture);
            maxHealthText.text = Mathf.CeilToInt(maxHealth).ToString(CultureInfo.InvariantCulture);
        }
    }
}
