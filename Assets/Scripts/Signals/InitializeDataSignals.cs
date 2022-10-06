using Data.ValueObject;
using Extentions;
using UnityEngine.Events;
using Data.ValueObject.LevelDatas;

namespace Signals
{
    public class InitializeDataSignals : MonoSingleton<InitializeDataSignals>
    {
        public UnityAction<BaseRoomDatas> onSaveBaseRoomData = delegate (BaseRoomDatas arg0) { };
        public UnityAction<MineBaseData> onSaveMineBaseData = delegate (MineBaseData arg0) { };
        public UnityAction<MilitaryBaseData> onSaveMilitaryBaseData = delegate (MilitaryBaseData arg0) { };
        public UnityAction<BuyablesData> onSaveBuyablesData = delegate (BuyablesData arg0) { };
        public UnityAction<int> onSaveLevelID = delegate (int arg0) { };


        public UnityAction<BaseRoomDatas> onLoadBaseRoomData = delegate (BaseRoomDatas arg0) { };
        public UnityAction<MineBaseData> onLoadMineBaseData = delegate (MineBaseData arg0) { };
        public UnityAction<MilitaryBaseData> onLoadMilitaryBaseData = delegate (MilitaryBaseData arg0) { };
        public UnityAction<BuyablesData> onLoadBuyablesData = delegate (BuyablesData arg0) { };
        public UnityAction<int> onLoadLevelID = delegate (int arg0) { };

    }
}