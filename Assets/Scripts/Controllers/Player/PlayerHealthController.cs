using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using Signals;
using Data.ValueObject;
using Enums;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{

    #region Self Variables

    #region Public Variables

    #endregion

    #region Serialized Variables

    [SerializeField]
    private PlayerManager playerManager;
    [SerializeField]
    private TextMeshProUGUI playerHealthText;
    [SerializeField]
    private Image healthBarFill;
    #endregion

    #region Private Variables

    private Camera cameraMain;
    private PlayerData _data;

    private int _health;

    private const int _increaseAmount = 1;

    #endregion

    #endregion
    private void Awake()
    {
        cameraMain = Camera.main;
    }

    public void SetHealthData(PlayerData data)
    {
        _data = data;
        Init();
        SetHealthText(_health);
    }
    private void Init()
    {
        _health = _data.PlayerHealth;
    }
    public async void IncreaseHealth()
    {
        if (playerManager.CurrentAreaType != AreaTypes.BaseDefense)
            return;

        if (_data.PlayerHealth == _health)
        {
            PlayerSignals.Instance.onHealthVisualClose?.Invoke();
            return;
        }
        _health += _increaseAmount;
        HealthUpdate(_health);

        await Task.Delay(50);
        IncreaseHealth();

    }
    public void TakeDamage(int damage)
    {
        _health -= damage;
        HealthUpdate(_health);
        if (_health <= 0) 
        {
            _health = 0;
            SetHealthText(_health);
            IsActive(false);
            _health = 1;
            playerManager.ResetPlayer();
            return; 
        }
        if (_health != 0) return;
        PlayerSignals.Instance.onHealthVisualClose?.Invoke();
    }
    private void Update()
    {
        transform.LookAt(transform.position + cameraMain.transform.rotation * Vector3.forward, cameraMain.transform.rotation * Vector3.up);
    }
    private void SetHealthText(int healthValue) => playerHealthText.text = healthValue.ToString();
    private void HealthUpdate(int healthValue)
    {
        healthBarFill.fillAmount = (float) healthValue /  _data.PlayerHealth;
        SetHealthText(healthValue);

        if (healthValue == _data.PlayerHealth)
        {
            IsActive(false);
        }
    }
    public void IsActive(bool value) => this.gameObject.SetActive(value);

}
