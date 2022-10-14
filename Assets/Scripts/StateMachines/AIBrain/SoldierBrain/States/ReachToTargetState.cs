using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Interfaces;

namespace StateMachines.AIBrain.Soldier.States
{
    public class ReachToTargetState : IState
    {
        private Animator _animator;
        private NavMeshAgent _navMeshAgent;
        private static readonly int _speed = Animator.StringToHash("Speed");

        public ReachToTargetState(Animator animator, NavMeshAgent navMeshAgent)
        {
            _animator = animator;
            _navMeshAgent = navMeshAgent;
        }
        public void Tick()
        {

        }
        public void OnEnter()
        {
            _navMeshAgent.speed = 1.801268E-05f;
            _animator.SetFloat(_speed, _navMeshAgent.velocity.magnitude);
        }
        public void OnExit()
        {

        }
    }
}
