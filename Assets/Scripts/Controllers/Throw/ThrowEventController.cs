using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using Enums;
using Signals;
using System.Threading.Tasks;
using StateMachines.AIBrain.Enemy;

public class ThrowEventController : MonoBehaviour, IReleasePoolObject, IGetPoolObject
{
    /// <summary>
    /// Call Animation Event
    /// </summary>

    #region Self Variables

    #region Private Variables

    private GameObject throwBomb;

    #endregion 

    #region Serializable Variables

    [SerializeField]
    private Transform bombHolder;

    [SerializeField]
    private BossEnemyBrain bossBrain;

    [SerializeField]
    private float forceImpulse;

    #endregion

    #endregion

    public void ThrowFunc()
    {
        Debug.Log("Throw in Time");
        if (throwBomb)
        {
            throwBomb.transform.SetParent(null);
            var rb = throwBomb.GetComponent<Rigidbody>();
            rb.useGravity = true;
            var dist = (bossBrain.PlayerTarget.position - bossBrain.transform.position).magnitude; // aramda ki mesafe magnitude yönsüz mesafe
            rb.AddForce(this.transform.forward * dist / forceImpulse, ForceMode.Impulse);
        }
    }

    public void SetAnimationWithBomb()
    {
        if(bossBrain.PlayerTarget)
            bossBrain.transform.LookAt(bossBrain.PlayerTarget, Vector3.up);

        throwBomb = GetObjectType(PoolType.Bomb);
        throwBomb.transform.SetParent(bombHolder);
        throwBomb.transform.position = bombHolder.position;
        throwBomb.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
        //ReleaseObject(throwBomb,PoolType.Bomb);
    }

    public void ReleaseObject(GameObject obj, PoolType poolType)
    {
        PoolSignals.Instance.onReleaseObjectFromPool?.Invoke(poolType, obj);
    }

    public GameObject GetObjectType(PoolType poolType)
    {
        return PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolType);  
    }
}
