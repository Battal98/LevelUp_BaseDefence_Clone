using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extentions;
using UnityEngine.Events;
using System;
using Enums;
using Data.ValueObject.AIDatas;

namespace Signals
{

    public class EnemySignals : MonoSingleton<EnemySignals>
    {

        public UnityAction<Transform> onEnemyDead = delegate { };
        public Func<EnemyType,EnemyTypeData> onGetEnemyAIData = delegate { return null; };

    } 
}
