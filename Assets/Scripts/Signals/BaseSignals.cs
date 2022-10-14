using Extentions;
using UnityEngine.Events;
using Enums;
using System;
using Data.ValueObject.LevelDatas;

namespace Signals
{
    public class BaseSignals : MonoSingleton<BaseSignals>
    {
        public UnityAction<BaseRoomTypes> onChangeExtentionVisibility = delegate { };
        public Func<BaseRoomTypes, RoomData> onSetRoomData;
        public UnityAction<RoomData, BaseRoomTypes> onUpdateRoomData = delegate { };
    }
}
