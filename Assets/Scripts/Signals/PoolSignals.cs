using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Extentions;
using Enums;
using System;

namespace Signals
{
    public class PoolSignals : MonoSingleton<PoolSignals>
    {
        public Func<PoolType, GameObject> onGetObjectFromPool = delegate { return null; };
        public UnityAction<PoolType,GameObject> onReleaseObjectFromPool = delegate { };
    } 
}
