using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using StateMachines.AIBrain.Soldier;
using Enums;
using Signals;
using Data.UnityObject;

namespace Controllers
{
    public class BulletPhysicsController : MonoBehaviour, IReleasePoolObject
    {
        private int bulletDamage = 20;
        public SoldierAIBrain soldierAIBrain;
        public Rigidbody Rigidbody;
        private void Awake()
        {
            //Rigidbody = GetComponentInParent<Rigidbody>();
        }
        private void OnEnable()
        {
            Invoke("Disable",0.7f);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damagable))
            {
                if (damagable.IsDead)
                    return;
                var health = damagable.TakeDamage(bulletDamage);
                Disable();
                if (health <= 0)
                {
                    damagable.IsDead = true;
                   // soldierAIBrain.RemoveTarget();
                }
                else
                {
                    //soldierAIBrain.EnemyTargetStatus();
                }
            }
        }
        public void ReleaseObject(GameObject obj, PoolType poolName)
        {
            PoolSignals.Instance.onReleaseObjectFromPool?.Invoke(poolName, obj);
        }
        protected void Disable()
        {
            Rigidbody.velocity = Vector3.zero;
            ReleaseObject(gameObject, PoolType.PistolBullet);
            gameObject.transform.position = Vector3.zero;
        }
    }
}
