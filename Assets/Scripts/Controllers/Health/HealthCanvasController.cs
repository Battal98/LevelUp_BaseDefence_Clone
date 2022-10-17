using TMPro;
using UnityEngine.UI;

namespace Controllers
{
    public class HealthCanvasController
    {
        private TextMeshProUGUI _healthText;
        private Image _fillImage;
        private int _maxHealth;
        private int _currentHealth;

        public HealthCanvasController(int maxHealth,int currentHealth,TextMeshProUGUI healthText, Image fillImage)
        {
            _healthText = healthText;
            _fillImage = fillImage;
            _maxHealth = maxHealth;
            _currentHealth = currentHealth;

        }

        public void SetHealthText()
        {
            _healthText.text = _currentHealth.ToString();
        }

        public void SetHealthBar()
        {
            _fillImage.fillAmount = _currentHealth / (float) _maxHealth;
        }

    } 
}
