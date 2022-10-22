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
        public UnityAction onOpenPortal = delegate { };
        public UnityAction<GameObject> onReleaseObjectUpdate = delegate { };

        public Func<EnemyType,EnemyTypeData> onGetEnemyAIDataWithType = delegate { return null; };
        public Func<EnemyAIData> onGetEnemyAIData = delegate { return null; };
        public Func<Transform> onGetSpawnTransform = delegate { return null; };
        public Func<Transform> onGetTargetTransform = delegate { return null; };

    } 
}
