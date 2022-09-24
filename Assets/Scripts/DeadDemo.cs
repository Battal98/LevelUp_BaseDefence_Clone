using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using Managers;
using Enums;
public class DeadDemo : MonoBehaviour, IGetPoolObject
{
    public GameObject GetObject(string poolName)
    {
        return  ObjectPoolManager.Instance.GetObject<GameObject>(poolName);
    }

    private void OnDisable()
    {
        for (int i = 0; i < 3; i++)
        {
            var creatableObj = GetObject(PoolType.Money.ToString());
            creatableObj.transform.position = this.transform.position;
        }

    }
}
