using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Data.UnityObject;
using Data.ValueObject.LevelDatas;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Interfaces;

public class DataInitManager : MonoBehaviour, ISavable
{
    #region Self Variables

    #region Public Variables

    #endregion

    #region Serialized Variables

    [SerializeField]
    private List<LevelData> levelDatas = new List<LevelData>();

    [SerializeField]
    private CD_Level cdLevel;

    [SerializeField]
    private CD_EnemyAI cdEnemy;

    #endregion

    #region Private Variables

    private int _levelID;
    private int _uniqueID = 12123;

    private LevelData _levelData;
    private BaseRoomDatas _baseRoomData;
    private MineBaseData _mineBaseData;
    private MilitaryBaseData _militaryBaseData;
    private BuyablesData _buyablesData;

    #endregion

    #endregion

    private CD_Level GetLevelDatas() => Resources.Load<CD_Level>("Data/CD_Level");


    private void Start()
    {
        InitLevelData();
        LevelSignals.Instance.onLevelInitialize?.Invoke();
    }

    #region Init Data Jobs

    private void InitLevelData()
    {
        cdLevel = GetLevelDatas();
        _levelID = cdLevel.LevelID;
        levelDatas = cdLevel.LevelDatas;
        if (!ES3.FileExists(this.cdLevel.GetKey()+$"{_uniqueID}.es3"))
        {
            if (!ES3.KeyExists(this.cdLevel.GetKey()))
            {
                cdLevel = GetLevelDatas();
                _levelID = cdLevel.LevelID;
                levelDatas = cdLevel.LevelDatas;
                Save(_uniqueID);
            }
        }
        Load(_uniqueID);
    }

    #endregion

    #region Event Subscriptions

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        InitializeDataSignals.Instance.onSaveLevelID += OnSyncLevelID;
        LevelSignals.Instance.onLevelInitialize += OnSyncLevel;
        InitializeDataSignals.Instance.onSaveBaseRoomData += SyncBaseRoomDatas;
        InitializeDataSignals.Instance.onSaveMineBaseData += SyncMineBaseDatas;
        InitializeDataSignals.Instance.onSaveMilitaryBaseData += SyncMilitaryBaseData;
        InitializeDataSignals.Instance.onSaveBuyablesData += SyncBuyablesData;
    }

    private void UnsubscribeEvents()
    {
        InitializeDataSignals.Instance.onSaveLevelID -= OnSyncLevelID;
        LevelSignals.Instance.onLevelInitialize -= OnSyncLevel;
        InitializeDataSignals.Instance.onSaveBaseRoomData -= SyncBaseRoomDatas;
        InitializeDataSignals.Instance.onSaveMineBaseData -= SyncMineBaseDatas;
        InitializeDataSignals.Instance.onSaveMilitaryBaseData -= SyncMilitaryBaseData;
        InitializeDataSignals.Instance.onSaveBuyablesData -= SyncBuyablesData;
    }
    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion

    private void SendDataManagers()
    {
        InitializeDataSignals.Instance.onLoadLevelID?.Invoke(_levelID);
        InitializeDataSignals.Instance.onLoadBaseRoomData?.Invoke(_baseRoomData);
        InitializeDataSignals.Instance.onLoadMineBaseData?.Invoke(_mineBaseData);
        InitializeDataSignals.Instance.onLoadMilitaryBaseData?.Invoke(_militaryBaseData);
        InitializeDataSignals.Instance.onLoadBuyablesData?.Invoke(_buyablesData);
    }
    #region Level Save - Load 

    public void Save(int uniqueId)
    {
        CD_Level cdLevel = new CD_Level(_levelID, levelDatas);
        SaveLoadSignals.Instance.onSaveGameData.Invoke(cdLevel, uniqueId);
    }

    public void Load(int uniqueId)
    {
        CD_Level cdLevel = SaveLoadSignals.Instance.onLoadGameData?.Invoke(this.cdLevel.GetKey(), uniqueId);
        Debug.Log(cdLevel);
        _levelID = cdLevel.LevelID;
        levelDatas = cdLevel.LevelDatas;
        _baseRoomData = cdLevel.LevelDatas[_levelID].BaseData.BaseRoomDatas;
        _mineBaseData = cdLevel.LevelDatas[_levelID].BaseData.MineBaseData;
        _militaryBaseData = cdLevel.LevelDatas[_levelID].BaseData.MilitaryBaseData;
        _buyablesData = cdLevel.LevelDatas[_levelID].BaseData.BuyablesData;

    }

    #endregion

    private void OnSyncLevel()
    {
        SendDataManagers();
    }

    #region Data Sync

    private void OnSyncLevelID(int levelID)
    {
        cdLevel.LevelID = levelID;
    }
    private void SyncBaseRoomDatas(BaseRoomDatas baseRoomData)
    {
        cdLevel.LevelDatas[_levelID].BaseData.BaseRoomDatas = baseRoomData;
    }

    private void SyncMineBaseDatas(MineBaseData mineBaseData)
    {
        cdLevel.LevelDatas[_levelID].BaseData.MineBaseData = mineBaseData;
    }

    private void SyncMilitaryBaseData(MilitaryBaseData militaryBaseData)
    {
        cdLevel.LevelDatas[_levelID].BaseData.MilitaryBaseData = militaryBaseData;
    }

    private void SyncBuyablesData(BuyablesData buyablesData)
    {
        cdLevel.LevelDatas[_levelID].BaseData.BuyablesData = buyablesData;
    }

    #endregion

}
