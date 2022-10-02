using UnityEngine;
using Interfaces;
using Enums;
using Signals;
public class DeadDemo : MonoBehaviour,IGetPoolObject
{
    public GameObject GetObjectType(PoolType poolName)
    {
        return  PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolName);
    }

    private void OnDisable()
    {
        for (int i = 0; i < 3; i++)
        {
            var creatableObj = GetObjectType(PoolType.Money);
            creatableObj.transform.position = this.transform.position;
        }

    }
}
