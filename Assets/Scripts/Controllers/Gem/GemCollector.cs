using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using Managers;
using Controllers;
using Signals;


public class GemCollector : MonoBehaviour
{
    [SerializeField]
    private List<int> gemCount = new List<int>();

    [SerializeField]
    private int gemValue = 1;
    private int moneyValue = 5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerPhysicsController>(out PlayerPhysicsController playerPhysicsController))
        {
            CoreGameSignals.Instance.onUpdateGemScore?.Invoke(gemValue);
            CoreGameSignals.Instance.onUpdateMoneyScore?.Invoke(moneyValue);
        }
    }
}
