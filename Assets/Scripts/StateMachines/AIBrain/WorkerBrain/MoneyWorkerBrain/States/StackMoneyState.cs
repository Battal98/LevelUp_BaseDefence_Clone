using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Interfaces;

namespace StateMachines.AIBrain.Workers.MoneyStates
{
    public class StackMoneyState : IState
    {
        private readonly NavMeshAgent _navmeshAgent;
        private readonly Animator _animator;
        private readonly MoneyWorkerAIBrain _moneyWorkerAIBrain;
        private readonly int _currentStock;
        private readonly int _maxCapacity;
        private readonly float _speed;

        public StackMoneyState(NavMeshAgent navMeshAgent, Animator animator, ref int currentMoneyStock, ref int totalMoneyCapacity, ref float speed, MoneyWorkerAIBrain moneyWorkerAIBrain)
        {
            _navmeshAgent = navMeshAgent;
            _animator = animator;
            _currentStock = currentMoneyStock;
            _maxCapacity = totalMoneyCapacity;
            _speed = speed;
            _moneyWorkerAIBrain = moneyWorkerAIBrain;
        }
        public void OnEnter()
        {
            Debug.Log(_moneyWorkerAIBrain);
            _navmeshAgent.SetDestination(_moneyWorkerAIBrain.CurrentTarget.transform.position);
        }

        public void OnExit()
        {
            
        }

        public void Tick()
        {

        }
    }
}
