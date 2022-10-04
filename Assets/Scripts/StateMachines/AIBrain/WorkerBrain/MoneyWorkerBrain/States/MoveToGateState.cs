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
        private readonly Transform _gateTarget;
        private static readonly int Speed = Animator.StringToHash("Speed");

        public bool IsArrive = false;
        public MoveToGateState(NavMeshAgent navMeshAgent, Animator animator,ref Transform gateTarget)
        {
            _navmeshAgent = navMeshAgent;
            _animator = animator;
            _gateTarget = gateTarget;
        }
        public void OnEnter()
        {
            //isWalking anim
            _navmeshAgent.SetDestination(_gateTarget.position);
            _navmeshAgent.speed = 1.53f;
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
