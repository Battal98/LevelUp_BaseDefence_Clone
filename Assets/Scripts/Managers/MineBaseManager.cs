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

        private Transform _instantiationPosition;
        private Transform _gemHolderPosition;
        

        #endregion

        #region Private Variables

        private int _currentLevel; //LevelManager uzerinden cekilecek
        private Transform _currentMineTarget;
        private int _currentGemAmount;
        private int _currentWorkerAmount;
        private int _maxWorkerAmount;
        public float GemCollectionOffset;
        private Dictionary<MinerAIBrain, GameObject> _mineWorkers=new Dictionary<MinerAIBrain, GameObject>();
        private MineBaseData _mineBaseData;
        

        #endregion

        #endregion
        private void Awake()
        {
            _mineBaseData=GetMineBaseData();
            AssignDataValues();
          
        }

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
                _currentObject.transform.position = _instantiationPosition.position;
                MinerAIBrain _currentMinerAIBrain=_currentObject.GetComponent<MinerAIBrain>();
                _mineWorkers.Add(_currentMinerAIBrain,_currentObject);
            }
        }

        private void AssignMinerValuesToDictionary()
        {
            for (int index = 0; index < _mineWorkers.Count; index++)
            {
                _mineWorkers.ElementAt(index).Key.GemCollectionOffset=GemCollectionOffset;
                _mineWorkers.ElementAt(index).Key.GemHolder= _gemHolderPosition;
            }
            
        }

        private void AssignDataValues()
        {
                _currentWorkerAmount =_mineBaseData.CurrentWorkerAmount;
                GemCollectionOffset=_mineBaseData.GemCollectionOffset;
                _maxWorkerAmount=_mineBaseData.MaxWorkerAmount;
                _gemHolderPosition = _mineBaseData.GemHolderPosition;
                _instantiationPosition = _mineBaseData.InstantiationPosition;


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
        }
       

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            MineBaseSignals.Instance.onGetRandomMineTarget -= GetRandomMineTarget;
            MineBaseSignals.Instance.onGetGemHolderPos -= OnGetGemHolderPos;
        }

        private Transform OnGetGemHolderPos()
        {
            return _gemHolderPosition;
        }

        #endregion

        public Tuple<Transform,GemMineType> GetRandomMineTarget()
        {
            int randomMineTargetIndex=Random.Range(0, _mineBaseData.MinePlaces.Count + _mineBaseData.CartPlaces.Count);
            return randomMineTargetIndex>= _mineBaseData.MinePlaces.Count
                ? Tuple.Create(_mineBaseData.CartPlaces[randomMineTargetIndex % _mineBaseData.CartPlaces.Count],GemMineType.Cart)
                :Tuple.Create(_mineBaseData.MinePlaces[randomMineTargetIndex],GemMineType.Mine);//Tuple ile enum donecek maden tipine gore animasyon degisecek stateler uzerinden
        }


        public MineBaseData GetMineBaseData() => Resources.Load<CD_Level>("Data/CD_Level").LevelDatas[_currentLevel].BaseData.MineBaseData;

        public GameObject GetObjectType(PoolType poolType)
        {
            return PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolType);
        }
    }
}