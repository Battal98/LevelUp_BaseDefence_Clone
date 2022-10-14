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
        [SerializeField]
        private EnemySpawnController enemySpawnController;

        #endregion

        #region Private Variables

        private const string _dataPath = "Data/CD_EnemyAI";

        #endregion

        #endregion

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            EnemySignals.Instance.onGetEnemyAIDataWithType += OnGetEnemyAIDataWithType;
            EnemySignals.Instance.onGetEnemyAIData += OnGetEnemyAIData;
            EnemySignals.Instance.onOpenPortal += OnOpenPortal;
            EnemySignals.Instance.onGetSpawnTransform += OnGetSpawnTransform;
            EnemySignals.Instance.onGetTargetTransform += OnGetTargetTransform;
            EnemySignals.Instance.onReleaseObjectUpdate += OnReleaseObjectUpdate;
        }

        private void UnsubscribeEvents()
        {
            EnemySignals.Instance.onGetEnemyAIDataWithType -= OnGetEnemyAIDataWithType;
            EnemySignals.Instance.onGetEnemyAIData -= OnGetEnemyAIData;
            EnemySignals.Instance.onOpenPortal -= OnOpenPortal;
            EnemySignals.Instance.onReleaseObjectUpdate -= OnReleaseObjectUpdate;
            EnemySignals.Instance.onGetSpawnTransform = OnGetSpawnTransform;
            EnemySignals.Instance.onGetTargetTransform = OnGetTargetTransform;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private Transform OnGetSpawnTransform()
        {
            return enemySpawnController.GetSpawnTransform();
        }

        private Transform OnGetTargetTransform()
        {
            return enemySpawnController.GetTargetTransform();
        }

        private EnemyTypeData OnGetEnemyAIDataWithType(EnemyType enemyType)
        {
            return Resources.Load<CD_EnemyAI>(_dataPath).EnemyAIData.EnemyList[(int)enemyType];
        }

        private EnemyAIData OnGetEnemyAIData()
        {
            return Resources.Load<CD_EnemyAI>(_dataPath).EnemyAIData;
        }

        private void OnOpenPortal()
        {
            portalController.OpenPortal();
        }

        private void OnReleaseObjectUpdate(GameObject obj)
        {
            enemySpawnController.ReleasedObjectCount(obj);
        }
    } 
}
