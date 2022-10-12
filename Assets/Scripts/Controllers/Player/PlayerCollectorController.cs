using Interfaces;
using UnityEngine;
using Signals;

namespace Controllers
{
    public class PlayerCollectorController : MonoBehaviour
    {
        [SerializeField] private StackerController moneyStackerController;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IStackable>(out IStackable stackable))
            {
                stackable.IsCollected = true;
                MoneyWorkerSignals.Instance.onThisMoneyTaken?.Invoke();
                moneyStackerController.SetStackHolder(stackable.SendToStack().transform);
                moneyStackerController.GetStack(stackable.SendToStack());
            }
            else if (other.TryGetComponent<Interactable>(out Interactable interactable)|| other.CompareTag("Gate"))
            {
                moneyStackerController.OnRemoveAllStack();
            }
        }
    }
}