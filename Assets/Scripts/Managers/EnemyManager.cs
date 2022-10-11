using UnityEngine;
using Signals;
using Data.ValueObject.AIDatas;
using Enums;
using Controllers;

namespace Managers
{
    public class EnemyManager : MonoBehaviour
    {
        #region Self variables 

        #region Public Variables



        #endregion

        #region Seriliazable Variables

        [SerializeField]
        private PortalController portalController;

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
            EnemySignals.Instance.onOpenPortal += OnOpenPortal;
        }

        private void UnsubscribeEvents()
        {
            EnemySignals.Instance.onGetEnemyAIData -= OnGetEnemyAIData;
            EnemySignals.Instance.onOpenPortal -= OnOpenPortal;
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

        private void OnOpenPortal()
        {
            portalController.OpenPortal();
        }
    } 
}
