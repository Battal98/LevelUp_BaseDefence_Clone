using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Interfaces
{
    public interface IStacker
    {
        void SetStackHolder(Transform otherTransform);
        void GetStack(GameObject stackableObj);
        void GetAllStack(IStack stack);
        void RemoveStack(IStackable stackable);

        void ResetStack(IStackable stackable);
    }
}