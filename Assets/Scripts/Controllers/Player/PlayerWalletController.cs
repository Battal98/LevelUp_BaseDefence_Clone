using Interfaces;
using UnityEngine;
using Signals;
using Concreate;
using Extentions;
using System.Threading.Tasks;

namespace Controllers
{
    public class PlayerWalletController : MonoBehaviour, ICustomer
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField]
        private StackerController moneyStackerController;

        #endregion

        #region Private Variables

        private bool _canPay = true;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Stackable>(out Stackable stackable))
            {
                CollectMoney(stackable);
               // CoreGameSignals.Instance.onUpdateMoneyScore.Invoke(+10);
            }
            if (other.TryGetComponent<StackableGem>(out StackableGem stackableGem))
            {

            }
            if (other.TryGetComponent<Interactable>(out Interactable interactable) || other.CompareTag("Gate"))
            {
                moneyStackerController.OnRemoveAllStack();
            }
        }
        private void CollectMoney(IStackable stackable)
        {
            moneyStackerController.SetStackHolder(stackable.SendToStack().transform);
            moneyStackerController.GetStack(stackable.SendToStack());
        }

        #region Paying Interaction
        public bool CanPay { get => CoreGameSignals.Instance.onHasEnoughMoney.Invoke(); set { } }
        public void MakePayment()
        {
            if (!CanPay)
            {
                CoreGameSignals.Instance.onStopMoneyPayment?.Invoke();
                return;
            }
            CoreGameSignals.Instance.onStartMoneyPayment?.Invoke();
        }


        #endregion
    }
}