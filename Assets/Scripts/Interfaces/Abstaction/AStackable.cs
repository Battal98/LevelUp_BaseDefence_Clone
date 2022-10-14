using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Signals;
using Extentions;

namespace Interfaces
{
    public abstract class AStackable : MonoBehaviour, IStackable
    {
        public virtual bool IsSelected { get; set; }
        public virtual bool IsCollected { get; set; }

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

        public virtual void SendStackable(Stackable stackableMoney)
        {
            DOVirtual.DelayedCall(0.1f, () => MoneyWorkerSignals.Instance.onSetStackable?.Invoke(stackableMoney));
        }

        public abstract GameObject SendToStack();

    }
}
