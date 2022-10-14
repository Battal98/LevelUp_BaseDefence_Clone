using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Interfaces;

namespace StateMachines.AIBrain.Soldier.States
{
    public class MoveToSlotState : IState
    {
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Vector3 _soldierPosition;
        private readonly Vector3 _slotPosition;
        private readonly float _stoppingDistance;
        private readonly SoldierAIBrain _soldierAIBrain;
        private readonly Animator _animator;
        private static readonly int Speed = Animator.StringToHash("Speed");
        private bool _hasReachToTarget;

        public MoveToSlotState(SoldierAIBrain soldierAIBrain, NavMeshAgent navMeshAgent, bool hasReachToTarget, Vector3 slotPosition, Animator animator)
        {
            _soldierAIBrain = soldierAIBrain;
            _navMeshAgent = navMeshAgent;
            _hasReachToTarget = hasReachToTarget;
            _slotPosition = slotPosition;
            _stoppingDistance = navMeshAgent.stoppingDistance;
            _animator = animator;
        }
        public void Tick()
        {
            _animator.SetFloat(Speed, _navMeshAgent.velocity.magnitude);
            if ((_navMeshAgent.transform.position - _slotPosition).sqrMagnitude < _stoppingDistance)
            {
                _hasReachToTarget = true;
                _soldierAIBrain.HasReachedSlotTarget = _hasReachToTarget;
            }
        }
        public void OnEnter()
        {
            _navMeshAgent.SetDestination(_slotPosition);
            _navMeshAgent.speed = 1.80f;
        }
        public void OnExit()
        {
            _navMeshAgent.enabled = false;
        }
    }
}
