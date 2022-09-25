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
                if (_moneyWorkerBrain.IsAvailable())
                {
                    MoneyWorkerSignals.Instance.onThisMoneyTaken?.Invoke(other.transform);
                    _moneyWorkerBrain.SetCurrentStock();
                    other.gameObject.transform.parent.gameObject.SetActive(false);

                    //stacking
                    //other'a layer deðiþtirme yapýlabilir
                }
            }
            if (other.CompareTag("Gate"))
            {
                _moneyWorkerBrain.RemoveAllStock();
            }
        }
    } 
}
