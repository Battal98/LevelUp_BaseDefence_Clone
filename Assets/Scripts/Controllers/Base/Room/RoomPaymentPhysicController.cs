using UnityEngine;

public class RoomPaymentPhysicController : MonoBehaviour
{
    [SerializeField] private RoomManager roomManager;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out ICustomer customer)) return;
        customer.MakePayment();
        roomManager.StartRoomPayment(customer.CanPay, customer);
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out ICustomer customer)) return;
        customer.CanPay = false;
        customer.MakePayment();
        roomManager.StopRoomPayment(false);
    }
}
