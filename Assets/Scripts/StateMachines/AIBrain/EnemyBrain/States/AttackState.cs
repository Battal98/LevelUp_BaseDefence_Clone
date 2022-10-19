using UnityEngine;
using UnityEngine.AI;
using Interfaces;
using System.Threading.Tasks;
using System;

namespace StateMachines.AIBrain.Enemy.States
{
    public class AttackState : IState
    {
        private readonly EnemyAIBrain _enemyAIBrain;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        private readonly float _attackRange;
        private static readonly int _attack = Animator.StringToHash("Attack");
        private static readonly int _run = Animator.StringToHash("Run");
        private float _attackTimer = 1f;
        private const float _refreshValue = 1f;

        private bool _inAttack;
        public bool InPlayerAttackRange() => _inAttack;
        public AttackState(NavMeshAgent navMeshAgent, Animator animator, EnemyAIBrain enemyAIBrain, float attackRange)
        {
            _navMeshAgent = navMeshAgent;
            _animator = animator;
            _enemyAIBrain = enemyAIBrain;
            _attackRange = attackRange;
        }
        public void OnEnter()
        {
            if (_enemyAIBrain.CurrentTarget)
            {
                _inAttack = true;
            }
        }

        public void OnExit()
        {
        }

        public void Tick()
        {
            if (_enemyAIBrain.CurrentTarget)
            {
                _navMeshAgent.destination =_enemyAIBrain.CurrentTarget.transform.position;
                _attackTimer -= Time.deltaTime;
                if (!(_attackTimer <= 0)) return;
                _enemyAIBrain.HitDamage();
                _animator.SetTrigger(_attack);
                _attackTimer = _refreshValue;
                CheckDistanceAttack();

            }
        }
        private void CheckDistanceAttack()
        {

            if (_navMeshAgent.remainingDistance > _attackRange)
                _inAttack = false;
        }
    }
}
