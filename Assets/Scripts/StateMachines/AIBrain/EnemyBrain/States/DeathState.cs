using UnityEngine;
using UnityEngine.AI;
using Interfaces;

namespace StateMachines.AIBrain.Enemy.States
{
    public class DeathState : IState
    {
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        public DeathState(NavMeshAgent navMeshAgent, Animator animator)
        {
            _navMeshAgent = navMeshAgent;
            _animator = animator;
        }
        public void OnEnter()
        {
            _navMeshAgent.enabled = false;
            _animator.SetTrigger("Die");
        }

        public void OnExit()
        {
            
        }

        public void Tick()
        {
            
        }
    }
}
