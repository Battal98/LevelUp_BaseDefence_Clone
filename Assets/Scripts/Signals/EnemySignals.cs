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

        public UnityAction<Vector3> onEnemyDead = delegate { };
        public Func<EnemyType,EnemyTypeData> onGetEnemyAIData = delegate { return null; };
        public Func<Transform, Vector3> onGetTransform = delegate { return Vector3.zero; };

    } 
}
