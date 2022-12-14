using UnityEngine;
using UnityEngine.AI;
using Interfaces;
using StateMachines.AIBrain.Enemy;

namespace StateMachines.AIBrain.Enemy.States
{
    public class ChaseState : IState
    {
        private readonly EnemyAIBrain _enemyAIBrain;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        private readonly float _attackRange;
        private readonly float _chaseSpeed;

        private static readonly string Move = "Move";
        private static readonly int Run = Animator.StringToHash("Run");

        private bool _inAttack = false;
        public bool InPlayerAttackRange() => _inAttack;

        public ChaseState(NavMeshAgent navMeshAgent, Animator animator, EnemyAIBrain enemyAIBrain, float attackRange, float chaseSpeed)
        {
            _navMeshAgent = navMeshAgent;
            _animator = animator;
            _enemyAIBrain = enemyAIBrain;
            _attackRange = attackRange;
            _chaseSpeed = chaseSpeed;
        }
        public void OnEnter()
        {
            if (_enemyAIBrain.CurrentTarget)
            {
                _inAttack = false;
                _navMeshAgent.speed = _chaseSpeed;
                _animator.SetTrigger(Run);
                _navMeshAgent.SetDestination(_enemyAIBrain.CurrentTarget.transform.position);
            }
        }

        public void OnExit()
        {

        }

        public void Tick()
        {
            if (_enemyAIBrain.CurrentTarget)
            {
                _navMeshAgent.destination = _enemyAIBrain.CurrentTarget.transform.position;
                _animator.SetFloat(Move, _navMeshAgent.velocity.magnitude);
                CheckDistanceChase();
            }
        }
        private void CheckDistanceChase()
        {
            if (_navMeshAgent.remainingDistance <= _attackRange)
                _inAttack = true;
        }
    }
}
