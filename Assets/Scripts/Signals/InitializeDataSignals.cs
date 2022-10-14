using Data.ValueObject;
using Extentions;
using UnityEngine.Events;
using Data.ValueObject.LevelDatas;
using Data.ValueObjects;
using System;

namespace Signals
{
    public class InitializeDataSignals : MonoSingleton<InitializeDataSignals>
    {
        public UnityAction<BaseRoomDatas> onSaveBaseRoomData = delegate (BaseRoomDatas arg0) { };
        public UnityAction<MineBaseData> onSaveMineBaseData = delegate (MineBaseData arg0) { };
        public UnityAction<MilitaryBaseData> onSaveMilitaryBaseData = delegate (MilitaryBaseData arg0) { };
        public UnityAction<BuyablesData> onSaveBuyablesData = delegate (BuyablesData arg0) { };
        public UnityAction<int> onSaveLevelID = delegate (int arg0) { };
        public UnityAction<ScoreData> onSaveGameScore = delegate (ScoreData arg0) { };


        public Func<MilitaryBaseData> onLoadMilitaryBaseData = delegate { return null; };
        public Func<BaseRoomDatas> onLoadBaseRoomData = delegate { return null; };
        public Func<BuyablesData> onLoadBuyablesData = delegate { return null; };
        public Func<MineBaseData> onLoadMineBaseData = delegate { return null; };
        public Func<ScoreData> onLoadGameScore = delegate { return default; };
        public UnityAction<int> onLoadLevelID = delegate (int arg0) { };

    }
}