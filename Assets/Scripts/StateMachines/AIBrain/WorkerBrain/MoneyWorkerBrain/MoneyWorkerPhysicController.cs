using System.Collections.Generic;
using UnityEngine;
using StateMachines.AIBrain.Workers;
using Signals;

namespace Controllers
{
    public class MoneyWorkerPhysicController : MonoBehaviour
    {
        [SerializeField]
        private MoneyWorkerAIBrain _moneyWorkerBrain;
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Collectables"))
            {
                Debug.Log("entry");
                MoneyWorkerSignals.Instance.onThisMoneyTaken?.Invoke(other.transform);
            }
        }
    } 
}
