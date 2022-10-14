using UnityEngine;
using Signals;

namespace Controllers
{
    public class SoldierCreatorController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerPhysicsController playerPhysicsController))
            {
                SoldierAISignals.Instance.onSoldierAmountUpgrade?.Invoke();
            }
        }
    } 
}
