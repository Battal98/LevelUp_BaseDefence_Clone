using UnityEngine;
using Interfaces;
using UnityEngine.AI;

namespace StateMachines.AIBrain.Enemy.States
{
    public class BaseAttackState : IState
    {
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        private readonly EnemyAIBrain _enemyAIBrain;
        private static readonly int Attack = Animator.StringToHash("Attack");
        private float _attackTimer = 2f;

        public BaseAttackState(NavMeshAgent agent, Animator animator)
        {
            _navMeshAgent = agent;
            _animator = animator;
        }
        public void Tick()
        {
            _attackTimer -= Time.deltaTime;
            if (!(_attackTimer <= 0)) return;
            _animator.SetTrigger(Attack);
            _attackTimer = 2f;
        }

        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }
    } 
}

