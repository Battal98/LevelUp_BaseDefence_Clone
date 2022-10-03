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

        public float AutoDestroyTime = 0.1f;
        public float MoveSpeed = 2f;
        public int Damage = 5;
        public Rigidbody Rigidbody;
        public CD_BulletTrail TrailConfig;
        protected TrailRenderer Trail;
        protected Transform Target;
        [SerializeField]
        private Renderer Renderer;


        protected const string DISABLE_METHOD_NAME = "Disable";
        protected const string DO_DISABLE_METHOD_NAME = "DoDisable";

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Trail = GetComponent<TrailRenderer>();
        }

        protected virtual void OnEnable()
        {
            if (Renderer != null)
            {
                Renderer.enabled = true;
            }

            CancelInvoke(DISABLE_METHOD_NAME);
            ConfigureTrail();
            Invoke(DISABLE_METHOD_NAME, AutoDestroyTime);
        }

        private void ConfigureTrail()
        {
            if (Trail != null && TrailConfig != null)
            {
                TrailConfig.SetupTrail(Trail);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamagable damagable))
            {

                if (damagable.IsDead)
                    return;
                var health = damagable.TakeDamage(bulletDamage);
                if (health <= 0)
                {
                    damagable.IsDead = true;
                    soldierAIBrain.RemoveTarget();
                    Disable();
                }
                else
                {
                    soldierAIBrain.EnemyTargetStatus();
                }
            }
        }
        public void ReleaseObject(GameObject obj, PoolType poolName)
        {
            PoolSignals.Instance.onReleaseObjectFromPool?.Invoke(poolName, obj);
        }
        protected void Disable()
        {
            CancelInvoke(DISABLE_METHOD_NAME);
            CancelInvoke(DO_DISABLE_METHOD_NAME);
            Rigidbody.velocity = Vector3.zero;
            if (Renderer != null)
            {
                Renderer.enabled = false;
            }
            if (Trail != null && TrailConfig != null)
            {
                Invoke(DO_DISABLE_METHOD_NAME, TrailConfig.Time);
            }
            else
            {
                DoDisable();
            }
            ReleaseObject(gameObject, PoolType.PistolBullet);
            gameObject.transform.position = Vector3.zero;
        }
        protected void DoDisable()
        {
            if (Trail != null && TrailConfig != null)
            {
                Trail.Clear();
            }
        }
    }
}
