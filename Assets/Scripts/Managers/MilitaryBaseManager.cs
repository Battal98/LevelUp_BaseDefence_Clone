using System.Collections.Generic;
using StateMachines.AIBrain.Soldier;
using Data.UnityObject;
using Data.ValueObject;
using Data.ValueObject.LevelDatas;
using Enums;
using Interfaces;
using Signals;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace Managers
{
    public class MilitaryBaseManager : MonoBehaviour, IGetPoolObject, IReleasePoolObject
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] 
        private Transform tentTransfrom;
        [SerializeField] 
        private Transform slotTransform;
        [SerializeField] 
        private GameObject slotPrefab;
        [SerializeField] 
        private Transform frontYardSoldierPosition;

        #endregion

        #region Private Variables

        private MilitaryBaseData _data;
        private bool _isBaseAvaliable;
        private bool _isTentAvaliable = true;
        private int _totalAmount;
        private int _soldierAmount;
        [ShowInInspector] private List<Vector3> _slotTransformList = new List<Vector3>();
        #endregion

        #endregion

        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            SoldierAISignals.Instance.onSoldierActivation += OnSoldierActivation;
            InitializeDataSignals.Instance.onLoadMilitaryBaseData += OnLoadMilitaryBaseData;
        }
        private void UnsubscribeEvents()
        {
            SoldierAISignals.Instance.onSoldierActivation -= OnSoldierActivation;
            InitializeDataSignals.Instance.onLoadMilitaryBaseData -= OnLoadMilitaryBaseData;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        private void OnLoadMilitaryBaseData(MilitaryBaseData militaryBaseData) => _data = militaryBaseData;


        private void OnSoldierActivation()
        {
            _isTentAvaliable = true;
        }
        public GameObject GetObjectType(PoolType poolName)
        {
            var soldierAIPrefab = PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolName);
            var soldierBrain = soldierAIPrefab.GetComponent<SoldierAIBrain>();
            SetSlotZoneTransformsToSoldiers(soldierBrain);
            return soldierAIPrefab;
        }
        private void SetSlotZoneTransformsToSoldiers(SoldierAIBrain soldierBrain)
        {
            soldierBrain.GetSlotTransform(_slotTransformList[_soldierAmount]);
            soldierBrain.TentPosition = tentTransfrom;
            soldierBrain.FrontYardStartPosition = frontYardSoldierPosition;
        }
        public void ReleaseObject(GameObject obj, PoolType poolName)
        {
            PoolSignals.Instance.onReleaseObjectFromPool?.Invoke(poolName, obj);
        }

        public void UpdateTotalAmount(int Amount)
        {
            if (!_isBaseAvaliable) return;
            if (_totalAmount < _data.BaseCapacity)
            {
                _totalAmount += Amount;
            }
            else
            {
                _isBaseAvaliable = false;
            }
        }

        [Button]
        public void UpdateSoldierAmount()
        {
            if (!_isTentAvaliable) return;
            if (_soldierAmount < _data.TentCapacity)
            {
                GetObjectType(PoolType.SoldierAI);
                _soldierAmount += 1;
            }
            else
            {
                _isTentAvaliable = false;
                _soldierAmount = 0;
            }
        }
        public void GetStackPositions(List<Vector3> gridPositionData)
        {
            for (int i = 0; i < gridPositionData.Count; i++)
            {
                _slotTransformList.Add(gridPositionData[i]);
                var obj = Instantiate(slotPrefab, gridPositionData[i], quaternion.identity, slotTransform);
            }
        }
    }
}