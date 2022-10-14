using Controllers;
using UnityEngine;
using Signals;
using Enums;
using Data.ValueObject.LevelDatas;
using System.Threading.Tasks;


public class RoomManager : MonoBehaviour
{
    [SerializeField]
    private BaseRoomTypes roomTypes;
    [SerializeField]
    private RoomPaymentTextController roomPaymentTextController;

    private RoomData _roomData;
    private int _payedAmount = 10;
    private bool _canTake;
    private void Start()
    {
        _roomData = GetRoomData();
        SetRoomCost(_roomData.RoomCost);
    }
    private RoomData GetRoomData() => BaseSignals.Instance.onSetRoomData(roomTypes);
    private void SetRoomCost(int cost) => roomPaymentTextController.SetInitText(cost);
    public void StartRoomPayment(bool canTake, ICustomer customer)
    {
        _canTake = canTake;
        if (!_canTake)
            return;
        UpdatePayment(customer);
    }
    public void StopRoomPayment(bool canTake) => _canTake = canTake;
    private async void UpdatePayment(ICustomer customer)
    {
        if (!_canTake || !customer.CanPay)
        {
            _canTake = true;
            CoreGameSignals.Instance.onStopMoneyPayment?.Invoke();
            return;
        }
        if (_roomData.RoomCost == 0)
        {
            _canTake = false;

            _roomData.AvailabilityType = AvabilityType.Unlocked;
            BaseSignals.Instance.onChangeExtentionVisibility(roomTypes);
            UpdateRoomData();
        }

        _roomData.RoomCost -= _payedAmount;
        CoreGameSignals.Instance.onStartMoneyPayment?.Invoke();
        roomPaymentTextController.UpdateText(_roomData.RoomCost);
        UpdateRoomData();
        await Task.Delay(100);
        UpdatePayment(customer);
    }

    private void UpdateRoomData() => BaseSignals.Instance.onUpdateRoomData(_roomData, roomTypes);
}
