using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Data.UnityObject;
using Data.ValueObject.LevelDatas;
using Data.ValueObject.AIDatas;
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

        InitializeDataSignals.Instance.onLoadMilitaryBaseData += OnLoadMilitaryBaseData;
        InitializeDataSignals.Instance.onLoadBaseRoomData += OnLoadBaseRoomData;
        InitializeDataSignals.Instance.onLoadBuyablesData += OnLoadBuyablesData;
        InitializeDataSignals.Instance.onLoadMineBaseData += OnLoadMineBaseData;
    }

    private void UnsubscribeEvents()
    {
        InitializeDataSignals.Instance.onSaveLevelID -= OnSyncLevelID;
        LevelSignals.Instance.onLevelInitialize -= OnSyncLevel;
        InitializeDataSignals.Instance.onSaveBaseRoomData -= SyncBaseRoomDatas;
        InitializeDataSignals.Instance.onSaveMineBaseData -= SyncMineBaseDatas;
        InitializeDataSignals.Instance.onSaveMilitaryBaseData -= SyncMilitaryBaseData;
        InitializeDataSignals.Instance.onSaveBuyablesData -= SyncBuyablesData;

        InitializeDataSignals.Instance.onLoadMilitaryBaseData -= OnLoadMilitaryBaseData;
        InitializeDataSignals.Instance.onLoadBaseRoomData -= OnLoadBaseRoomData;
        InitializeDataSignals.Instance.onLoadBuyablesData -= OnLoadBuyablesData;
        InitializeDataSignals.Instance.onLoadMineBaseData -= OnLoadMineBaseData;
    }
    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion

    private void SendDataManagers()
    {
        InitializeDataSignals.Instance.onLoadLevelID?.Invoke(_levelID);
    }
    #region Level Save - Load 

    public void Save(int uniqueId)
    {
        CD_Level cdLevel = new CD_Level(_levelID, levelDatas);
        SaveLoadSignals.Instance.onSaveLevelData.Invoke(cdLevel, uniqueId);
    }

    public void Load(int uniqueId)
    {
        CD_Level cdLevel = SaveLoadSignals.Instance.onLoadLevelData?.Invoke(this.cdLevel.GetKey(), uniqueId);
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

    #region Level Data Sync

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

    private MilitaryBaseData OnLoadMilitaryBaseData()
    {
        return _militaryBaseData;
    }
    private BaseRoomDatas OnLoadBaseRoomData()
    {
        return _baseRoomData;
    }
    private MineBaseData OnLoadMineBaseData()
    {
        return _mineBaseData;
    }
    private BuyablesData OnLoadBuyablesData()
    {
        return _buyablesData;
    }

    #endregion

}
