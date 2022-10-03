using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Interfaces;
using System;
using Signals;

namespace StateMachines.AIBrain.Workers.MoneyStates
{
    public class StackMoneyState : IState
    {
        private readonly NavMeshAgent _navmeshAgent;
        private readonly Animator _animator;
        private readonly MoneyWorkerAIBrain _moneyWorkerAIBrain;
        private bool isArrive;

        public Func<bool> IsArriveToMoney() => () => isArrive && _moneyWorkerAIBrain.IsAvailable();

        public StackMoneyState(NavMeshAgent navMeshAgent, Animator animator, MoneyWorkerAIBrain moneyWorkerAIBrain)
        {
            _navmeshAgent = navMeshAgent;
            _animator = animator;
            _moneyWorkerAIBrain = moneyWorkerAIBrain;
        }
        public void OnEnter()
        {
        }

        public void OnExit()
        {
            isArrive = false;
        }
        public void Tick()
        {
            if (_navmeshAgent.remainingDistance <= 0f)
            {
                _moneyWorkerAIBrain.CurrentTarget = null;
                isArrive = true;
            }
           
        }
    }
}
