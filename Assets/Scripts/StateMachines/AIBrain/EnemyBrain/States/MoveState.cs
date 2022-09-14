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
        private readonly Transform _turretTarget;
        private static readonly int Speed = Animator.StringToHash("Speed");

        public MoveState(NavMeshAgent navMeshAgent, Animator animator, EnemyAIBrain enemyAIBrain , float moveSpeed, ref Transform turretTarget)
        {
            _navMeshAgent = navMeshAgent;
            _animator = animator;
            _enemyAIBrain = enemyAIBrain;
            _moveSpeed = moveSpeed;
            _turretTarget = turretTarget;
        }
        public void OnEnter()
        {
            if (_turretTarget)
            {
                _navMeshAgent.enabled = true;
                _navMeshAgent.speed = _moveSpeed;
                _navMeshAgent.SetDestination(_turretTarget.position);
                _animator.SetTrigger("Walk");
            }
            //_animator.SetInteger(Speed, 1);
        }

        public void OnExit()
        {
            //_animator.SetInteger(Speed, 0);
        }

        public void Tick()
        {

        }
    } 
}
