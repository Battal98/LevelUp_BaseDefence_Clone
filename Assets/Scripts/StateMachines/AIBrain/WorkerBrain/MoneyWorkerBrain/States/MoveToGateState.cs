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
        private readonly int _currentStock;
        private readonly int _maxCapacity;
        private readonly float _speed;

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
        }

        public void OnExit()
        {
            IsArrive = false;
        }

        public void Tick()
        {
            if (_navmeshAgent.remainingDistance <= 0.1f)
            {
                IsArrive=true;
            }
        }
    } 
}
