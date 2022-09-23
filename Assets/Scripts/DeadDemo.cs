using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Signals;

public class DeadDemo : MonoBehaviour
{
    private void OnDisable()
    {
        EnemySignals.Instance.onEnemyDead?.Invoke(EnemySignals.Instance.onGetTransform.Invoke(this.transform));
    }
}
