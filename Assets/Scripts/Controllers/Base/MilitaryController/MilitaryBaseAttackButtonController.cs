using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Signals;

public class MilitaryBaseAttackButtonController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer backgroundSprite;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SoldierAISignals.Instance.onSoldierActivation?.Invoke();
            backgroundSprite.color = new Color(1, 0, 0, 0.5f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            backgroundSprite.color = new Color(0, 0, 0, 0.5f);
        }
    }
}
