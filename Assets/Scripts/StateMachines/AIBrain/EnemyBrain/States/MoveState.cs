using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Interfaces;

namespace StateMachines.AIBrain.Enemy.States
{
    public class MoveState : IState
    {
        private readonly EnemyAIBrain _enemyAIBrain;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        private readonly float _moveSpeed;


        private Vector3 _lastPos = Vector3.zero;

        private static readonly int Speed = Animator.StringToHash("Speed");

        public float TimeStuck;

        public MoveState(NavMeshAgent navMeshAgent, Animator animator, EnemyAIBrain enemyAIBrain , float moveSpeed)
        {
            _navMeshAgent = navMeshAgent;
            _animator = animator;
            _enemyAIBrain = enemyAIBrain;
            _moveSpeed = moveSpeed; 
        }
        public void OnEnter()
        {
            TimeStuck = 0;
            _navMeshAgent.enabled = true;
            _navMeshAgent.speed = _moveSpeed;
            var randomTarget = Random.Range(0, _enemyAIBrain.TurretTargetList.Count);
            _navMeshAgent.SetDestination(_enemyAIBrain.TurretTargetList[randomTarget].position);
            //_animator.SetInteger(Speed, 1);
        }

        public void OnExit()
        {
            //_animator.SetInteger(Speed, 0);
        }

        public void Tick()
        {
            if (Vector3.Distance(_enemyAIBrain.transform.position, _lastPos) <= 0f)
                TimeStuck += Time.deltaTime;

            _lastPos = _enemyAIBrain.transform.position;
        }
    } 
}
