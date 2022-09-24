using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Interfaces;

namespace StateMachines.AIBrain.Workers.MoneyStates
{
    public class SearchState : IState
    {
        private readonly NavMeshAgent _navmeshAgent;
        private readonly Animator _animator;
        private readonly MoneyWorkerAIBrain _moneyWorkerAIBrain;
        private readonly int _currentStock;
        private readonly int _maxCapacity;
        public SearchState(NavMeshAgent navMeshAgent, Animator animator, ref int currentMoneyStock, ref int totalMoneyCapacity, MoneyWorkerAIBrain moneyWorkerAIBrain)
        {
            _navmeshAgent = navMeshAgent;
            _animator = animator;
            _currentStock = currentMoneyStock;
            _maxCapacity = totalMoneyCapacity;
            _moneyWorkerAIBrain = moneyWorkerAIBrain;
        }
        public void OnEnter()
        {
        }

        public void OnExit()
        {
            
        }

        public void Tick()
        {
            
        }


    } 
}
