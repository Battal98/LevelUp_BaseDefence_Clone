using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Signals;
using System;
using Data.ValueObject.AIDatas;
using Enums;

namespace Managers
{
    public class EnemyManager : MonoBehaviour
    {
        #region Self variables 

        #region Public Variables



        #endregion

        #region Seriliazable Variables

        
        #endregion

        #region Private Variables


        #endregion

        #endregion

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            EnemySignals.Instance.onGetEnemyAIData += OnGetEnemyAIData;
        }

        private void UnsubscribeEvents()
        {
            EnemySignals.Instance.onGetEnemyAIData -= OnGetEnemyAIData;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        private EnemyTypeData OnGetEnemyAIData(EnemyType enemyType)
        {
            return Resources.Load<CD_EnemyAI>("Data/CD_EnemyAI").EnemyAIData.EnemyList[(int)enemyType];
        }
    } 
}
