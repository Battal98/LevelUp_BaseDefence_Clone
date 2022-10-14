using System.Collections.Generic;
using StateMachines.AIBrain.Soldier;
using Data.ValueObject;
using Data.ValueObject.LevelDatas;
using Enums;
using Interfaces;
using Signals;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;
using System.Threading.Tasks;
using System.Collections;

namespace Managers
{
    public class MilitaryBaseManager : MonoBehaviour, IGetPoolObject
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
        private Transform frontYardPosition;

        [SerializeField]
        private GameObject slotZonePrefab;
        #endregion

        #region Private Variables

        private MilitaryBaseData _data;
        private SoldierAIData _soldierAIData;
        private bool _isBaseAvaliable;
        private bool _isTentAvaliable = true;
        private int _totalAmount;
        private int _soldierAmount;
        [ShowInInspector] private List<Vector3> _slotTransformList = new List<Vector3>();
        private int _tentCapacity;
        private List<SoldierAIBrain> _soldierList = new List<SoldierAIBrain>();

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetBaseData();
        }
        private MilitaryBaseData GetBaseData()
        {
            return InitializeDataSignals.Instance.onLoadMilitaryBaseData.Invoke();
        }
        public IEnumerator Start()
        {
            if (_data.CurrentSoldierAmount == 0)
                yield break;
            yield return new WaitForSeconds(1f);
            StartCoroutine(soldierEnumerator());
            yield return new WaitForSeconds(3f);
            StopCoroutine(soldierEnumerator());
        }
        private IEnumerator soldierEnumerator()
        {
            OnSoldiersInit(_data.CurrentSoldierAmount);
            yield return null;
        }
        private void OnSoldiersInit(int soldierCount)
        {
            for (var i = 0; i < soldierCount; i++)
            {
                GetSoldier();
            }
        }

        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            SoldierAISignals.Instance.onSoldierActivation += OnSoldierActivation;
            SoldierAISignals.Instance.onSoldierAmountUpgrade += OnSoldierAmountUpgrade;
        }
        private void UnsubscribeEvents()
        {
            SoldierAISignals.Instance.onSoldierActivation -= OnSoldierActivation;
            SoldierAISignals.Instance.onSoldierAmountUpgrade -= OnSoldierAmountUpgrade;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        private void OnSoldierActivation()
        {
            var soldierCount = _soldierList.Count - 1;
            for (var i = 0; i < soldierCount + 1; i++)
            {
                _soldierList[soldierCount - i].HasSoldiersActivated = true;
                _soldierList.RemoveAt(soldierCount - i);
                _soldierList.TrimExcess();
            }
            _isTentAvaliable = true;
            _data.CurrentSoldierAmount = 0;
        }
        private void GetSoldier()
        {
            var soldierAIPrefab = GetObject(PoolType.SoldierAI);
            var soldierBrain = soldierAIPrefab.GetComponent<SoldierAIBrain>();
            _soldierList.Add(soldierBrain);
            SetSlotZoneTransformsToSoldiers(soldierBrain);
        }

        private void SetSlotZoneTransformsToSoldiers(SoldierAIBrain soldierBrain)
        {
            soldierBrain.GetSlotTransform(_slotTransformList[_data.CurrentSoldierAmount]);
            soldierBrain.TentPosition = tentTransfrom;
            soldierBrain.FrontYardStartPosition = frontYardPosition;
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
        private void OnSoldierAmountUpgrade()
        {
            UpdateSoldierAmount();
        }
        [Button]
        private async void UpdateSoldierAmount()
        {
            if (!_isTentAvaliable) return;
            if (_data.CurrentSoldierAmount < _data.TentCapacity)
            {
                GetSoldier();
                _data.CurrentSoldierAmount += 1;
                await Task.Delay(100);
                UpdateSoldierAmount();
            }
            else
            {
                _isTentAvaliable = false;
                _data.CurrentSoldierAmount = 0;
            }
        }
        public void GetStackPositions(List<Vector3> gridPositionData)
        {
            for (int i = 0; i < gridPositionData.Count; i++)
            {
                _slotTransformList.Add(gridPositionData[i]);
                var obj = Instantiate(slotZonePrefab, gridPositionData[i], quaternion.identity, slotTransform);
            }
        }

        #region Pool Signals

        public GameObject GetObject(PoolType poolName)
        {
            return PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolName);
        }
        #endregion

        #region SaveSignals

        [Button]
        private void SaveData()
        {
            InitializeDataSignals.Instance.onSaveMilitaryBaseData.Invoke(_data);
        }
        private void OnApplicationQuit()
        {
            InitializeDataSignals.Instance.onSaveMilitaryBaseData.Invoke(_data);
        }
        #endregion
    }
}