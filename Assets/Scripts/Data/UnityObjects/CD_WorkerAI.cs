using UnityEngine;
using Data.ValueObject.AIDatas;
using Interfaces;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_WorkerAI", menuName = "BaseDefence/CD_WorkerAI", order = 0)]
    public class CD_WorkerAI : ScriptableObject, ISavableEntity
    {
        public WorkerAIData WorkerAIData;
        private string Key = "MoneyWorkerAI";

        public string GetKey()
        {
            return Key;
        }
    } 
}
