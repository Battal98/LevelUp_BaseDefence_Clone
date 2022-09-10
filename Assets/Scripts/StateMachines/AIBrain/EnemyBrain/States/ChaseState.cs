using System.Collections;
using System.Collections.Generic;
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
            _inAttack = false;
            _navMeshAgent.speed = _chaseSpeed;
            _navMeshAgent.SetDestination(_enemyAIBrain.PlayerTarget.transform.position);
        }

        public void OnExit()
        {

        }

        public void Tick()
        {
            _navMeshAgent.destination = _enemyAIBrain.PlayerTarget.transform.position;
            CheckDistanceChase();
        }
        private void CheckDistanceChase()
        {
            if (_navMeshAgent.remainingDistance <= _attackRange)
                _inAttack = true;
        }
    }
}
