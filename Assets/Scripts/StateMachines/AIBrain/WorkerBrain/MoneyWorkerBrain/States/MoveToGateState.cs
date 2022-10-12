using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Interfaces;

namespace StateMachines.AIBrain.Workers.MoneyStates
{
    public class MoveToGateState : IState
    {
        private readonly NavMeshAgent _navmeshAgent;
        private readonly Animator _animator;
        private readonly Vector3 _gateTarget;
        private static readonly int Speed = Animator.StringToHash("Speed");
        private readonly float _speed;

        public bool IsArrive = false;
        public MoveToGateState(NavMeshAgent navMeshAgent, Animator animator,Vector3 gateTarget, float maxSpeed)
        {
            _navmeshAgent = navMeshAgent;
            _animator = animator;
            _gateTarget = gateTarget;
            _speed = maxSpeed;
        }
        public void OnEnter()
        {
            //isWalking anim
            _navmeshAgent.SetDestination(_gateTarget);
            _navmeshAgent.speed = _speed;
        }

        public void OnExit()
        {
            IsArrive = false;
        }

        public void Tick()
        {
            _animator.SetFloat(Speed, _navmeshAgent.velocity.magnitude);
            if (_navmeshAgent.remainingDistance <= 0.1f)
            {
                IsArrive=true;
            }
        }
    } 
}
