using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Signals;

namespace Interfaces
{
    public abstract class AStackable : MonoBehaviour, IStackable
    {

        public virtual void SetInit(Transform initTransform, Vector3 position)
        {

        }

        public virtual void SetVibration(bool isVibrate)
        {

        }

        public virtual void SetSound()
        {

        }

        public virtual void EmitParticle()
        {

        }

        public virtual void PlayAnimation()
        {

        }

        public virtual void SendPosition(Transform transform)
        {
            DOVirtual.DelayedCall(0.1f, () => MoneyWorkerSignals.Instance.onSetMoneyPosition?.Invoke(transform));
        }

        public abstract GameObject SendToStack();

    }
}
