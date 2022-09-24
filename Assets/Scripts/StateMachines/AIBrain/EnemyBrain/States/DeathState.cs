using UnityEngine;
using UnityEngine.AI;
using Interfaces;
using Signals;
using StateMachines.AIBrain.Workers;
using Managers;
using Enums;
using DG.Tweening;

namespace StateMachines.AIBrain.Enemy.States
{
    public class DeathState : IState, IGetPoolObject, IReleasePoolObject
    {
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        private readonly EnemyAIBrain _brain;
        private readonly EnemyType _type;
        public DeathState(NavMeshAgent navMeshAgent, Animator animator, EnemyAIBrain brain, EnemyType type)
        {
            _navMeshAgent = navMeshAgent;
            _animator = animator;
            _brain = brain; 
            _type = type;
        }

        public GameObject GetObject(string poolName)
        {
           return ObjectPoolManager.Instance.GetObject<GameObject>(poolName);
        }
        public void ReleaseObject(GameObject obj, string poolName)
        {
            ObjectPoolManager.Instance.ReturnObject<GameObject>(obj, poolName);
        }

        public void OnEnter()
        {
            _navMeshAgent.enabled = false;
            _animator.SetTrigger("Die");
            DOVirtual.DelayedCall(1f, () => ReleaseObject(_brain.gameObject, _type.ToString()));
            for (int i = 0; i < 3; i++)
            {
                var creatableObj = GetObject(PoolType.Money.ToString());
                creatableObj.transform.position = _brain.transform.position;
            }

        }

        public void OnExit()
        {
            
        }

        public void Tick()
        {
            
        }
    }
}
