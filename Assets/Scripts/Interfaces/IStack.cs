using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;

namespace Interfaces
{
    public interface IStack
    {
        void SetStackHolder(GameObject gameObject);
        void SetGrid();
        void SendGridDataToStacker();

    }
}