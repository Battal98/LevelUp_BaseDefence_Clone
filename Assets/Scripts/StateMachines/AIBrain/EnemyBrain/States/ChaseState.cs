using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Interfaces;

namespace StateMachines.AIBrain.Enemy.States
{
    public class ChaseState : IState
    {
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;

        public ChaseState(NavMeshAgent navMeshAgent, Animator animator)
        {
            _navMeshAgent = navMeshAgent;
            _animator = animator;
        }
        public void OnEnter()
        {
            throw new System.NotImplementedException();
        }

        public void OnExit()
        {
            throw new System.NotImplementedException();
        }

        public void Tick()
        {
            throw new System.NotImplementedException();
        }
    }
}
