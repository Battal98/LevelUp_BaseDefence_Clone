using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Signals;

public class MilitaryBaseAttackButtonController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SoldierAISignals.Instance.onSoldierActivation?.Invoke();
        }
    }
}
