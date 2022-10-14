using System;
using System.Collections.Generic;
using UnityEngine;
using Signals;
using System.Linq;
using Data.ValueObject.AIDatas;
using Data.UnityObject;
using Enums;
using Sirenix.OdinInspector;
using StateMachines.AIBrain.Workers;
using Interfaces;
using Extentions;
using Controllers;

namespace Managers
{
    public class MoneyWorkerManager : MonoBehaviour ,IGetPoolObject, IReleasePoolObject
    {
        #region Self variables 

        #region Private Variables

        [ShowInInspector]
        private List<Stackable> _targetList = new List<Stackable>();
        [ShowInInspector]
        private List<MoneyWorkerAIBrain> _workerList = new List<MoneyWorkerAIBrain>();
        [ShowInInspector]
        private List<Vector3> _slotTransformList = new List<Vector3>();

        #endregion

        #endregion

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            MoneyWorkerSignals.Instance.onGetMoneyAIData += OnGetWorkerAIData;
            MoneyWorkerSignals.Instance.onGetTransformMoney += OnSendMoneyPositionToWorkers;
            MoneyWorkerSignals.Instance.onThisMoneyTaken += OnThisMoneyTaken;
            MoneyWorkerSignals.Instance.onSetStackable += OnAddStackableToList;
        }

        private void UnsubscribeEvents()
        {
            MoneyWorkerSignals.Instance.onGetMoneyAIData -= OnGetWorkerAIData;
            MoneyWorkerSignals.Instance.onThisMoneyTaken -= OnThisMoneyTaken;
            MoneyWorkerSignals.Instance.onSetStackable -= OnAddStackableToList;
            MoneyWorkerSignals.Instance.onGetTransformMoney -= OnSendMoneyPositionToWorkers;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion

        private WorkerAITypeData OnGetWorkerAIData(WorkerType type)
        {
            return Resources.Load<CD_WorkerAI>("Data/CD_WorkerAI").WorkerAIData.WorkerAITypes[(int)type];
        }

        private void OnAddStackableToList(Stackable pos)
        {
            _targetList.Add(pos);
        }

        private Transform OnSendMoneyPositionToWorkers(Transform workerTransform)
        {
            if (_targetList.Count == 0)
                return null;

            var _targetT = _targetList.OrderBy(t => (t.transform.position - workerTransform.transform.position).sqrMagnitude)
            .Where(t => !t.IsSelected)
            .Take(_targetList.Count - 1)
            .LastOrDefault();
            _targetT.IsSelected = true;
            return _targetT.transform;
        }

        private void SendMoneyPositionToWorkers(Transform workerTransform)
        {
            OnSendMoneyPositionToWorkers(workerTransform);
        }

        private void OnThisMoneyTaken()
        {
            var removedObj = _targetList.Where(t => t.IsCollected).FirstOrDefault();
            _targetList.Remove(removedObj);
            _targetList.TrimExcess();

            foreach (var t in _workerList.Where(t => t.CurrentTarget == removedObj))
            {
                SendMoneyPositionToWorkers(t.transform);
            }
        }

        public void GetStackPositions(List<Vector3> gridPos)
        {
            for (int i = 0; i < gridPos.Count; i++)
            {
                _slotTransformList.Add(gridPos[i]);
            }
        }

        private void SetWorkerPosition(MoneyWorkerAIBrain workerAIBrain)
        {
            workerAIBrain.SetInitPosition(_slotTransformList[0]);
            _slotTransformList.RemoveAt(0);
            _slotTransformList.TrimExcess();
        }

        [Button("Add Money Worker")]
        private void CreateMoneyWorker()
        {
            if (OnGetWorkerAIData(WorkerType.MoneyWorkerAI).CurrentWorkerValue == 5)
                return;
            var obj = GetObject(PoolType.MoneyWorkerAI);
            var objComp = obj.GetComponent<MoneyWorkerAIBrain>();
            _workerList.Add(objComp);
            SetWorkerPosition(objComp);
        }

        [Button("Release Worker")]
        private void ReleaseMoneyWorker()
        {
            if (_workerList[0])
            {
                var obj = _workerList[0];
                ReleaseObject(obj.gameObject, PoolType.MoneyWorkerAI);
                _workerList.Remove(obj);
            }
        }
        public GameObject GetObject(PoolType poolName)
        {
           return PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolName);
        }

        public void ReleaseObject(GameObject obj, PoolType poolName)
        {
            PoolSignals.Instance.onReleaseObjectFromPool?.Invoke(poolName, obj);
        }

    }
}
