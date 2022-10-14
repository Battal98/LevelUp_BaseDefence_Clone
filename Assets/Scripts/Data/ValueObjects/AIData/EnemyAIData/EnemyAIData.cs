using System.Collections.Generic;
using System;

namespace Data.ValueObject.AIDatas
{
    [Serializable]
    public class EnemyAIData
    {
        public List<EnemyTypeData> EnemyList;
        public EnemySpawnData enemySpawnData;
    }
}
