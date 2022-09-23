using UnityEngine;
using UnityEngine.AI;
using Interfaces;
using Signals;
using StateMachines.AIBrain.Workers;

namespace StateMachines.AIBrain.Enemy.States
{
    public class DeathState : IState
    {
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        private readonly EnemyAIBrain _brain;
        public DeathState(NavMeshAgent navMeshAgent, Animator animator, EnemyAIBrain brain)
        {
            _navMeshAgent = navMeshAgent;
            _animator = animator;
            _brain = brain; 
        }
        public void OnEnter()
        {
            _navMeshAgent.enabled = false;
            _animator.SetTrigger("Die");
            EnemySignals.Instance.onEnemyDead?.Invoke(EnemySignals.Instance.onGetTransform.Invoke(_brain.transform));
        }

        public void OnExit()
        {
            
        }

        public void Tick()
        {
            
        }
    }
}
