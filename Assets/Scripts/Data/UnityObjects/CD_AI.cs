using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.ValueObject.AIDatas;

[CreateAssetMenu(fileName = "CD_AI", menuName = "BaseDefence/CD_AI", order = 0)]
public class CD_AI : ScriptableObject
{
    public EnemyAIData EnemyAIData;
    public WorkerAIData WorkerAIData;
}
