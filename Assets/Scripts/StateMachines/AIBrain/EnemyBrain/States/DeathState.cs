using UnityEngine;
using UnityEngine.AI;
using Interfaces;
using Signals;
using StateMachines.AIBrain.Workers;
using Managers;
using Enums;
using DG.Tweening;
using System;

namespace StateMachines.AIBrain.Enemy.States
{
    public class DeathState : IState, IGetPoolObject, IReleasePoolObject
    {
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        private readonly EnemyAIBrain _brain;
        private readonly string _type;
        public DeathState(NavMeshAgent navMeshAgent, Animator animator, EnemyAIBrain brain, string type)
        {
            _navMeshAgent = navMeshAgent;
            _animator = animator;
            _brain = brain; 
            _type = type;
        }
        public GameObject GetObject(PoolType poolName)
        {
            return PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolName);
        }
        public void ReleaseObject(GameObject obj, PoolType poolName)
        {
            PoolSignals.Instance.onReleaseObjectFromPool?.Invoke(poolName,obj);
        }

        public void OnEnter()
        {
            var poolType = (PoolType)Enum.Parse(typeof(PoolType), _type);
            
            _navMeshAgent.enabled = false;
            _animator.SetTrigger("Die");
            _brain.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.grey;

            EnemySignals.Instance.onReleaseObjectUpdate?.Invoke(_brain.gameObject);
            ParticleSignals.Instance.onPlayParticleWithSetColor?.Invoke(ParticleType.EnemyDeath,_brain.transform.position,_brain.transform.rotation,Color.red);
            
            EnemyDoDead(poolType);
            
            for (int i = 0; i < 3; i++)
            {
                var creatableObj = GetObject(PoolType.Money);
                creatableObj.transform.position = _brain.transform.position;
            }
        }

        public void OnExit()
        {
            
        }

        public void Tick()
        {
            
        }

        private void EnemyDoDead(PoolType type)
        {
            DOVirtual.DelayedCall(1f, () =>
            {
                _brain.transform.DOMoveY(-3f, 1f).OnComplete(() => 
                {
                    ReleaseObject(_brain.gameObject, type); 
                });
            });
        }
    }
}
