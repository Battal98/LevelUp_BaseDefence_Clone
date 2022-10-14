using UnityEngine;
using Enums;
using System;
namespace Interfaces
{
    public interface IGetPoolObject
    {
        GameObject GetObject(PoolType poolType);
    }
}
