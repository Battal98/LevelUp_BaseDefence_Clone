using System.Collections;
using System.Collections.Generic;
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
            _inAttack = true;
            _navMeshAgent.SetDestination(_enemyAIBrain.PlayerTarget.transform.position);
        }

        public void OnExit()
        {
        }

        public void Tick()
        {
            _navMeshAgent.destination = _enemyAIBrain.PlayerTarget.transform.position;
            CheckDistanceAttack();
        }
        private void CheckDistanceAttack()
        {
            if (_navMeshAgent.remainingDistance > _attackRange)
                _inAttack = false;
        }
    }
}
