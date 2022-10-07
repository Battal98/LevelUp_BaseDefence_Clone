using System;
using System.Collections.Generic;
using System.Linq;
using Data.UnityObject;
using Data.ValueObjects;
using Data.ValueObject.LevelDatas;
using Enums;
using Interfaces;
using Signals;
using UnityEngine;
using Random = UnityEngine.Random;
using StateMachines.AIBrain.Workers;



namespace Managers
{
    public class MineBaseManager : MonoBehaviour, IGetPoolObject
    {
        #region Self Variables

        #region Public Variables



        #endregion

        #region Serialized Variables
        [SerializeField]
        private Transform instantiationPosition;
        [SerializeField]
        private Transform gemHolderPosition;
        [SerializeField]
        private List<Transform> minePlaces;
        [SerializeField]
        private List<Transform> cartPlaces;


        #endregion

        #region Private Variables

        private Dictionary<MinerAIBrain, GameObject> _mineWorkers=new Dictionary<MinerAIBrain, GameObject>();
        private int _currentWorkerAmount;
        private float _gemCollectionOffset;

        private MineBaseData _mineBaseData;
        

        #endregion

        #endregion

        private void Start()
        {
            InstantiateAllMiners();
            AssignMinerValuesToDictionary();
        }
       

        private void InstantiateAllMiners()
        {
            for (int index = 0; index < _currentWorkerAmount; index++)
            {
                GameObject _currentObject = GetObjectType(PoolType.MinerWorkerAI);
                _currentObject.transform.position = instantiationPosition.position;
                MinerAIBrain _currentMinerAIBrain=_currentObject.GetComponent<MinerAIBrain>();
                _mineWorkers.Add(_currentMinerAIBrain,_currentObject);
            }
        }

        private void AssignMinerValuesToDictionary()
        {
            for (int index = 0; index < _mineWorkers.Count; index++)
            {
                _mineWorkers.ElementAt(index).Key.GemCollectionOffset=_gemCollectionOffset;
                _mineWorkers.ElementAt(index).Key.GemHolder= gemHolderPosition;
            }
            
        }

        private void AssignDataValues()
        {
                _currentWorkerAmount =_mineBaseData.CurrentWorkerAmount;
                _gemCollectionOffset=_mineBaseData.GemCollectionOffset;
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            MineBaseSignals.Instance.onGetRandomMineTarget += GetRandomMineTarget;
            MineBaseSignals.Instance.onGetGemHolderPos += OnGetGemHolderPos;

            InitializeDataSignals.Instance.onLoadMineBaseData += OnLoadMineBaseData;

        }
        private void UnSubscribeEvents()
        {
            InitializeDataSignals.Instance.onLoadMineBaseData -= OnLoadMineBaseData;
            MineBaseSignals.Instance.onGetRandomMineTarget -= GetRandomMineTarget;
            MineBaseSignals.Instance.onGetGemHolderPos -= OnGetGemHolderPos;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void OnLoadMineBaseData(MineBaseData mineBaseData) 
        { 
            _mineBaseData = mineBaseData;
            AssignDataValues();
        }


        private Transform OnGetGemHolderPos()
        {
            return gemHolderPosition;
        }

        #endregion

        public Tuple<Transform,GemMineType> GetRandomMineTarget()
        {
            int randomMineTargetIndex=Random.Range(0, minePlaces.Count + cartPlaces.Count);
            return randomMineTargetIndex>= minePlaces.Count
                ? Tuple.Create(cartPlaces[randomMineTargetIndex % cartPlaces.Count],GemMineType.Cart)
                :Tuple.Create(minePlaces[randomMineTargetIndex],GemMineType.Mine);//Tuple ile enum donecek maden tipine gore animasyon degisecek stateler uzerinden
        }

        public GameObject GetObjectType(PoolType poolType)
        {
            return PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolType);
        }
    }
}