using Interfaces;
using UnityEngine;

namespace Controllers
{
    public class PlayerCollectorController : MonoBehaviour
    {
        [SerializeField] private MoneyStackerController moneyStackerController;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IStackable>(out IStackable stackable))
            {
                moneyStackerController.SetStackHolder(stackable.SendToStack().transform);
                moneyStackerController.GetStack(stackable.SendToStack());
            }
            else if (other.TryGetComponent<Interactable>(out Interactable interactable))
            {
                moneyStackerController.OnRemoveAllStack();
            }
        }
    }
}