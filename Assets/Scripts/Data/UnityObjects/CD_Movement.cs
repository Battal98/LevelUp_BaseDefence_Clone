using Data.ValueObject;
using Keys;
using UnityEngine;

namespace Data.UnityObject
{
    public abstract class CD_Movement : ScriptableObject
    {
        public abstract void DoMovement(ref bool _isReadyToMove, ref Rigidbody _rigidbody,
            ref InputParams inputparams, ref PlayerDatas _moveData);
    }
}