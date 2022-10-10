using UnityEngine;
using Interfaces;
using Signals;
using Enums;
using Managers;

public class BombCollisionController : MonoBehaviour, IReleasePoolObject
{
    [SerializeField]
    private ParticleSystem bombParticle;

    public void ReleaseObject(GameObject obj, PoolType poolType)
    {
        PoolSignals.Instance.onReleaseObjectFromPool(poolType,obj);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerManager>(out PlayerManager manager) || collision.gameObject.CompareTag("Ground") )
        {
            Debug.Log("Boooom");
            bombParticle.transform.SetParent(null);
            bombParticle.transform.localScale= Vector3.one * 3f;
            bombParticle.Play();
            ReleaseObject(this.gameObject, PoolType.Bomb);
        }

    }
}
