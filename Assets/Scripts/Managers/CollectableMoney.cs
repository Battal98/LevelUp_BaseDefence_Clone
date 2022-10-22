using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Signals;
using Extentions;

public class CollectableMoney : MonoBehaviour
{
    [SerializeField] private Stackable stackableMoney;
    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        PlayerSignals.Instance.onResetPlayerStack += OnResetPlayerStack;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void UnsubscribeEvents()
    {
        PlayerSignals.Instance.onResetPlayerStack -= OnResetPlayerStack;
    }

    private void OnResetPlayerStack()
    {
        stackableMoney.OpenPhysics();
    }
}
