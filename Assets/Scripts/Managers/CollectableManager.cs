using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Signals;
using DG.Tweening;

namespace Managers
{
    public class CollectableManager : MonoBehaviour
    {
        private void OnEnable()
        {
            DOVirtual.DelayedCall( 0.1f,()=> MoneyWorkerSignals.Instance.onSetMoneyPosition?.Invoke(this.transform));
        }
    } 
}
