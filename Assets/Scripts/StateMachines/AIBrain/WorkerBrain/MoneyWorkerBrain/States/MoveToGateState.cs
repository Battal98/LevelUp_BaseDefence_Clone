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
        public MoveToGateState(NavMeshAgent navMeshAgent, Animator animator, ref int currentMoneyStock, ref int totalMoneyCapacity, ref float speed,ref Transform gateTarget)
        {
            _navmeshAgent = navMeshAgent;
            _animator = animator;
            _currentStock = currentMoneyStock;
            _maxCapacity = totalMoneyCapacity;
            _speed = speed; 
            _gateTarget = gateTarget;
        }
        public void OnEnter()
        {
            IsArrive = false;
            _navmeshAgent.SetDestination(_gateTarget.position);
        }

        public void OnExit()
        {
            
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
