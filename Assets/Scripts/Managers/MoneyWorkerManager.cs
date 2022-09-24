using System.Collections;
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

namespace Managers
{
    public class MoneyWorkerManager : MonoBehaviour, IGetPoolObject
    {
        #region Self variables 

        #region Private Variables

        [ShowInInspector]
        private List<Transform> _targetList = new List<Transform>();
        [ShowInInspector]
        private List<MoneyWorkerAIBrain> _workerList = new List<MoneyWorkerAIBrain>();

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
        }

        private void UnsubscribeEvents()
        {
            MoneyWorkerSignals.Instance.onGetMoneyAIData -= OnGetWorkerAIData;
            MoneyWorkerSignals.Instance.onThisMoneyTaken -= OnThisMoneyTaken;
            MoneyWorkerSignals.Instance.onSetMoneyPosition -= OnAddMoneyPositionToList;
            MoneyWorkerSignals.Instance.onGetTransformMoney -= OnSendMoneyPositionToWorkers;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private WorkerAITypeData OnGetWorkerAIData(WorkerType type)
        {
            return Resources.Load<CD_WorkerAI>("Data/CD_WorkerAI").WorkerAIData.WorkerAITypes[(int)type];
        }

        private void OnAddMoneyPositionToList(Transform pos)
        {
            _targetList.Add(pos);
        }

        private Transform OnSendMoneyPositionToWorkers(Transform workerTransform)
        {
            var transform = _targetList.OrderBy(t => Vector3.Distance(t.transform.position, workerTransform.position)).FirstOrDefault();
            _targetList.Remove(transform);
            return transform; 
        }

        private void SendMoneyPositionToWorkers(Transform workerTransform)
        {
            OnSendMoneyPositionToWorkers(workerTransform);
        }

        private void OnThisMoneyTaken(Transform gO)
        {
            for (int i = 0; i < _workerList.Count; i++)
            {
                if (_workerList[i].CurrentTarget == gO)
                {
                    SendMoneyPositionToWorkers(_workerList[i].transform);
                }
                else
                {
                    if (_targetList.Contains(gO))
                    {
                        _targetList.Remove(gO);
                    }
                }
            }
        }
        [Button("Add Money Worker")]
        private void CreateMoneyWorker()
        {
            var obj = GetObject(PoolType.MoneyWorkerAI.ToString());
            _workerList.Add(obj.GetComponent<MoneyWorkerAIBrain>());
        }

        public GameObject GetObject(string poolName)
        {
           return ObjectPoolManager.Instance.GetObject<GameObject>(poolName);
        }
        #endregion
    }
}
