using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Signals;
using System;
using Data.ValueObject.AIDatas;
using Data.UnityObject;
using Enums;
using Sirenix.OdinInspector;

namespace Managers
{
    public class WorkerManager : MonoBehaviour
    {
        #region Self variables 

        #region Public Variables



        #endregion

        #region Seriliazable Variables



        #endregion

        #region Private Variables

        [ShowInInspector]
        private List<Vector3> _targetList = new List<Vector3>();

        #endregion

        #endregion

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            WorkerSignals.Instance.onGetMoneyAIData += OnGetWorkerAIData;
            EnemySignals.Instance.onEnemyDead += OnGetEnemyPositon;

            
        }

        private void UnsubscribeEvents()
        {
            WorkerSignals.Instance.onGetMoneyAIData -= OnGetWorkerAIData;
            EnemySignals.Instance.onEnemyDead -= OnGetEnemyPositon;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private WorkerAITypeData OnGetWorkerAIData(WorkerType type)
        {
            return Resources.Load<CD_WorkerAI>("Data/CD_WorkerAI").WorkerAIData.WorkerAITypes[(int)type];
        }

        private void OnGetEnemyPositon(Vector3 pos)
        {
            _targetList.Add(pos);
        }

        private void OnSendEnemyPositionToWorkers()
        {

        }

        /// TODO: Send data to workers from here. 
        /// TODO2: Spawn controller etc. manage here.
        /// TODO3: Send enemies Pos to workers from here


        #endregion
    }
}
