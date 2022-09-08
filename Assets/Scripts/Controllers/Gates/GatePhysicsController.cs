using UnityEngine;
using Enums;

namespace Controllers
{
    public class GatePhysicsController : MonoBehaviour
    {
        [SerializeField]
        private Collider gateWallCollider;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Hostile"))
            {
                gateWallCollider.enabled = false;
                GateSignals.Instance.onChangeGateState(GateType.Open);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Hostile"))
            {
                gateWallCollider.enabled = true;
                GateSignals.Instance.onChangeGateState(GateType.Close);
            }
        }
    } 
}
