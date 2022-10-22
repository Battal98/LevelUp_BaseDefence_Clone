using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using TMPro;
using UnityEngine.UI;

namespace Managers
{
    public class HealthManager : MonoBehaviour
    {

       /* #region Serializable Variables

        [SerializeField]
        private TextMeshProUGUI _healthText;
        [SerializeField]
        private Image _fillImage;

        #endregion

        #region Private Variables

        private int _currentHealth;
        private int _maxHealth;
        private HealthController healthController;
        private HealthCanvasController healthCanvasController; 
        //private 

        #endregion

        private void Awake()
        {

            healthController = new HealthController();
            healthCanvasController = new HealthCanvasController(_maxHealth, _currentHealth,_healthText, _fillImage);

            //_maxHealth = 
        }
        

        private void OnEnable()
        {
            _currentHealth = _maxHealth;
        }

        private void SetInitAllComponents()
        {

        }

        private void SetHealthText()
        {
            healthCanvasController.SetHealthText();
        }

        private void UpdateHealthBar()
        {
            healthCanvasController.SetHealthBar();
        }
       */
        public void Update()
        {
            Camera camera = Camera.main;
            transform.LookAt(transform.position + camera.transform.rotation * Vector3.back, camera.transform.rotation * Vector3.up);
        }

    } 
}
