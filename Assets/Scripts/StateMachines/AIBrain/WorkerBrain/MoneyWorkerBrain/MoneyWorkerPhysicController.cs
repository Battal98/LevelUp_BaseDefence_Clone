using System.Collections.Generic;
using UnityEngine;
using StateMachines.AIBrain.Workers;

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
                /*if (!_moneyWorkerBrain.MoneyList.Contains(other.gameObject))
                {
                }*/
                    //stacking yapacak 
            }
        }
    } 
}
