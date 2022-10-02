using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Interfaces
{
    public abstract class AStacker : MonoBehaviour, IStacker
    {
        public List<GameObject> StackList = new List<GameObject>();

        public virtual void SetStackHolder(Transform otherTransform)
        {
            otherTransform.SetParent(transform);
        }

        public virtual void GetStack(GameObject stackableObj)
        {

        }


        public virtual void GetAllStack(IStack stack)
        {

        }

        public virtual void RemoveStack(IStackable stackable)
        {

        }


        public virtual void ResetStack(IStackable stackable)
        {

        }

    }
}