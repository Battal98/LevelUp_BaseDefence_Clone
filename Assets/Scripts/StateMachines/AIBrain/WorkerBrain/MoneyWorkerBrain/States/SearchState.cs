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

        public SearchState(NavMeshAgent navMeshAgent, Animator animator, MoneyWorkerAIBrain moneyWorkerAIBrain)
        {
            _navmeshAgent = navMeshAgent;
            _animator = animator;
            _moneyWorkerAIBrain = moneyWorkerAIBrain;
        }
        public void OnEnter()
        {
            
            if (_moneyWorkerAIBrain.IsAvailable())
            {
                _moneyWorkerAIBrain.StartSearch(true);
            }
        }

        public void OnExit()
        {
            _moneyWorkerAIBrain.StartSearch(false);
        }

        public void Tick()
        {
            
        }


    } 
}
