using UnityEngine;
using Data.ValueObject.AIDatas;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_WorkerAI", menuName = "BaseDefence/CD_WorkerAI", order = 0)]
    public class CD_WorkerAI : ScriptableObject
    {
        public WorkerAIData WorkerAIData;
    } 
}
