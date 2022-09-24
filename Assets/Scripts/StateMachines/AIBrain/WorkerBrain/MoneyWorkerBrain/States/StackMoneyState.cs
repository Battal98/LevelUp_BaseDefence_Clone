using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Interfaces;
using System;

namespace StateMachines.AIBrain.Workers.MoneyStates
{
    public class StackMoneyState : IState
    {
        private readonly NavMeshAgent _navmeshAgent;
        private readonly Animator _animator;
        private readonly MoneyWorkerAIBrain _moneyWorkerAIBrain;
        private bool isArrive;

        public Func<bool> IsArriveToMoney() => () => isArrive;

        public StackMoneyState(NavMeshAgent navMeshAgent, Animator animator, MoneyWorkerAIBrain moneyWorkerAIBrain)
        {
            _navmeshAgent = navMeshAgent;
            _animator = animator;
            _moneyWorkerAIBrain = moneyWorkerAIBrain;
        }
        public void OnEnter()
        {
            isArrive = false;
            SetNewDestination();
        }

        public void OnExit()
        {
            
        }

        public void Tick()
        {
            if (_navmeshAgent.stoppingDistance <= 0.1f)
            {
                isArrive = true;
            }
        }
        private void SetNewDestination()
        {
            _moneyWorkerAIBrain.CurrentTarget = _moneyWorkerAIBrain.GetMoneyPosition();
        }
    }
}
