using System.Collections;
using System.Collections.Generic;
using Extentions;
using UnityEngine.Events;
using Enums;

namespace Signals
{
    public class BaseSignals : MonoSingleton<BaseSignals>
    {
        public UnityAction<BaseRoomTypes> onChangeExtentionVisibility = delegate { };
    }
}
