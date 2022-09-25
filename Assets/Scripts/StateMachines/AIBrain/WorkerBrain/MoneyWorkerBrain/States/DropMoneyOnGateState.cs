using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Interfaces;

namespace StateMachines.AIBrain.Workers.MoneyStates
{
    public class DropMoneyOnGateState : IState
    {
        private readonly NavMeshAgent _navmeshAgent;
        private readonly Animator _animator;
        private readonly MoneyWorkerAIBrain _moneyWorkerAIBrain;
        private readonly Transform _startPos;
        public DropMoneyOnGateState(NavMeshAgent navMeshAgent, Animator animator, MoneyWorkerAIBrain moneyWorkerAIBrain, ref Transform startPos)
        {
            _navmeshAgent = navMeshAgent;
            _animator = animator;
            _moneyWorkerAIBrain = moneyWorkerAIBrain;
            _startPos = startPos;
        }
        public void OnEnter()
        {
            _navmeshAgent.SetDestination(_startPos.position);

        }

        public void OnExit()
        {
            
        }

        public void Tick()
        {
           
        }
    }
}
