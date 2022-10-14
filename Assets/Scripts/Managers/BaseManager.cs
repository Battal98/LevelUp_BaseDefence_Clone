using UnityEngine;
using Controllers;
using Data.UnityObject;
using Data.ValueObject.LevelDatas;
using Signals;
using Enums;
using System.Linq;

namespace Managers
{
    public class BaseManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] 
        private BaseExtentionController extentionController;
        [SerializeField] 
        private BaseTextController baseTextController;

        #endregion

        #region Private Variables

        private BaseRoomDatas baseRoomData;

        #endregion

        #endregion

        private void Awake()
        {
            baseRoomData = GetData();
            SetExistingRooms();
            baseTextController.SetBaseLevelText();
        }


        #region Event Subscription

        private void OnEnable() => SubscribeEvents();
        private void SubscribeEvents()
        {
            BaseSignals.Instance.onChangeExtentionVisibility += OnChangeVisibility;
            BaseSignals.Instance.onSetRoomData += OnSetRoomData;
            BaseSignals.Instance.onUpdateRoomData += OnUpdateRoomData;

        }
        private void UnsubscribeEvents()
        {
            BaseSignals.Instance.onChangeExtentionVisibility -= OnChangeVisibility;
            BaseSignals.Instance.onSetRoomData += OnSetRoomData;
            BaseSignals.Instance.onUpdateRoomData += OnUpdateRoomData;
        }
        private void OnDisable() => UnsubscribeEvents();
        #endregion  
        private BaseRoomDatas GetData() => InitializeDataSignals.Instance.onLoadBaseRoomData?.Invoke();

        private void SaveData() => InitializeDataSignals.Instance.onSaveBaseRoomData?.Invoke(baseRoomData);

        private void SetExistingRooms()
        {
            foreach (var t in baseRoomData.Rooms.Where(t => t.AvailabilityType == AvabilityType.Unlocked))
            {
                ChangeVisibility(t.BaseRoomTypes);
            }
        }

        private void OnUpdateRoomData(RoomData roomData, BaseRoomTypes roomTypes)
        {
            baseRoomData.Rooms[(int)roomTypes] = roomData;
            SaveData();
        }

        private RoomData OnSetRoomData(BaseRoomTypes roomTypes) => baseRoomData.Rooms[(int)roomTypes];
        private void OnChangeVisibility(BaseRoomTypes roomTypes)
        {
            ChangeVisibility(roomTypes);
            ChangeAvailabilityType(roomTypes);
        }

        private void ChangeAvailabilityType(BaseRoomTypes roomTypes)
        {
            baseRoomData.Rooms[(int)roomTypes].AvailabilityType = AvabilityType.Unlocked;
            InitializeDataSignals.Instance.onSaveBaseRoomData?.Invoke(baseRoomData);
        }

        private void ChangeVisibility(BaseRoomTypes roomTypes) => extentionController.ChangeExtentionVisibility(roomTypes);


    }
}
