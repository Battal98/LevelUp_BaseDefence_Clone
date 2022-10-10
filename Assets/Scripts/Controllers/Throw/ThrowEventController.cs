using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using Enums;
using Signals;
using System.Threading.Tasks;
using StateMachines.AIBrain.Enemy;
using Data.ValueObjects;

public class ThrowEventController : MonoBehaviour, IReleasePoolObject, IGetPoolObject
{
    /// <summary>
    /// Call Animation Event
    /// </summary>

    #region Self Variables

    #region Private Variables

    private GameObject _throwBomb;
    private ThrowData _throwData;
    #endregion 

    #region Serializable Variables

    [SerializeField]
    private Transform bombHolder;

    [SerializeField]
    private BossEnemyBrain bossBrain;


    [SerializeField]
    private bool isPathActiveRunTime = true;

    [SerializeField]
    private GameObject spriteTarget;

    #endregion

    #endregion

    private void Awake()
    {
        _throwData = Resources.Load<CD_Throw>("Data/CD_Throw").ThrowData;
    }

    private void Update()
    {
        if(isPathActiveRunTime && bossBrain.PlayerTarget && _throwBomb)
            DrawPath();
    }
    public void ThrowFunc()
    {
        Debug.Log("Throw in Time");
        if (_throwBomb)
        {
            _throwBomb.transform.SetParent(null);
            var rb = _throwBomb.GetComponent<Rigidbody>();
            rb.useGravity = true;
            Throw();
        }
    }

    public void SetAnimationWithBomb()
    {
        
        if (bossBrain.PlayerTarget)
            bossBrain.transform.LookAt(bossBrain.PlayerTarget, Vector3.up);

        _throwBomb = GetObjectType(PoolType.Bomb);
        _throwBomb.transform.SetParent(bombHolder);
        _throwBomb.transform.position = bombHolder.position;
        _throwBomb.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
        //ReleaseObject(throwBomb,PoolType.Bomb);
    }

    private void Throw()
    {
        spriteTarget.SetActive(true);
        spriteTarget.transform.position = bossBrain.PlayerTarget.position + new Vector3(0, 0.2f, 0);
        _throwBomb.transform.SetParent(null);
        var rb = _throwBomb.GetComponent<Rigidbody>();
        Physics.gravity = Vector3.up * _throwData.Gravity;
        rb.useGravity = true;
        rb.velocity = CalculateThrowData().initialVelocity;
    }

    private ThrowInputData CalculateThrowData()
    {
        float distY = bossBrain.PlayerTarget.position.y - _throwBomb.transform.position.y; // y (yukseklik)'de ki yer degistirme
        Vector3 distXZ = new Vector3(bossBrain.PlayerTarget.position.x - _throwBomb.transform.position.x, 0, bossBrain.PlayerTarget.position.z - _throwBomb.transform.position.z); //  x and z yer degistirme
        float time = Mathf.Sqrt(-2 * _throwData.Height / _throwData.Gravity) + Mathf.Sqrt(2 * (distY - _throwData.Height) / _throwData.Gravity); // kok (-2 * yukseklik / yercekimi kuvveti) + kok (2* (yukseklik fark� - offset yukseklik) / yercekimi kuvveti)
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * _throwData.Gravity * _throwData.Height); // y de ki velocity hesabi
        Vector3 velocityXZ = distXZ / time; // zamana bagli olarak alacagi yol 

        Invoke("DeactiveSpriteTargetDelay", time);


        return new ThrowInputData(velocityXZ + velocityY * -Mathf.Sign(_throwData.Gravity), time);
    }
    private void DeactiveSpriteTargetDelay()
    {
        spriteTarget.SetActive(false);
    }

    private void DrawPath()
    {
        ThrowInputData launchData = CalculateThrowData();
        Vector3 previousDrawPoint = _throwBomb.transform.position;

        int resolution = 30;
        for (int i = 1; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * _throwData.Gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = _throwBomb.transform.position + displacement;
            Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
            previousDrawPoint = drawPoint;
        }
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
