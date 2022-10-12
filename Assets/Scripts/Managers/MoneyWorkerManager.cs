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
        private List<StackableMoney> _targetList = new List<StackableMoney>();
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
            MoneyWorkerSignals.Instance.onSetMoneyPosition += OnAddMoneyPositionToList;
            //MoneyWorkerSignals.Instance.onSendWaitPosition += OnSendWaitPosition;
        }

        private void UnsubscribeEvents()
        {
            MoneyWorkerSignals.Instance.onGetMoneyAIData -= OnGetWorkerAIData;
            MoneyWorkerSignals.Instance.onThisMoneyTaken -= OnThisMoneyTaken;
            MoneyWorkerSignals.Instance.onSetMoneyPosition -= OnAddMoneyPositionToList;
            MoneyWorkerSignals.Instance.onGetTransformMoney -= OnSendMoneyPositionToWorkers;
            //MoneyWorkerSignals.Instance.onSendWaitPosition -= OnSendWaitPosition;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private WorkerAITypeData OnGetWorkerAIData(WorkerType type)
        {
            return Resources.Load<CD_WorkerAI>("Data/CD_WorkerAI").WorkerAIData.WorkerAITypes[(int)type];
        }
        /*private Vector3 OnSendWaitPosition()
        {
            return _slotTransformList;
        }*/

        private void OnAddMoneyPositionToList(StackableMoney pos)
        {
            _targetList.Add(pos);
        }

        private Transform OnSendMoneyPositionToWorkers(Transform workerTransform)
        {
            if (_targetList.Count == 0)
                return null;

            var _targetT = _targetList.OrderBy(t => Vector3.Distance(t.transform.position, workerTransform.transform.position))
            .Where(t => !t.IsSelected)
            .Take(_targetList.Count)
            .OrderBy(t => UnityEngine.Random.Range(0, int.MaxValue))
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

            for (int i = 0; i < _workerList.Count; i++)
            {
                if (_workerList[i].CurrentTarget == removedObj)
                {
                    SendMoneyPositionToWorkers(_workerList[i].transform);
                }
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
            var obj = GetObjectType(PoolType.MoneyWorkerAI) ;
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
        public GameObject GetObjectType(PoolType poolName)
        {
           return PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolName);
        }

        public void ReleaseObject(GameObject obj, PoolType poolName)
        {
            PoolSignals.Instance.onReleaseObjectFromPool?.Invoke(poolName, obj);
        }
        #endregion
    }
}
