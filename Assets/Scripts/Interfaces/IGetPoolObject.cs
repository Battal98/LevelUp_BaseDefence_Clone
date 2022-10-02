using UnityEngine;
using Enums;
using System;
namespace Interfaces
{
    public interface IGetPoolObject
    {
        GameObject GetObjectType(PoolType poolType);
    }
}
