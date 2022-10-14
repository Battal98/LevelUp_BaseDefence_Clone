using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Interfaces;

namespace StateMachines.AIBrain.Soldier.States
{
    public class IdleState : IState
    {
        private Transform _tentPosition;
        private NavMeshAgent _navMeshAgent;
        public IdleState(Transform tentPosition, NavMeshAgent navMeshAgent)
        {
            _tentPosition = tentPosition;
            _navMeshAgent = navMeshAgent;
        }
        public void Tick()
        {

        }
        public void OnEnter()
        {
            GetTentSpawnPosition();
            _navMeshAgent.enabled = true;

        }
        public void OnExit()
        {
        }
        private void GetTentSpawnPosition()
        {
            bool TentSpawnPosition(Vector3 center, out Vector3 result)
            {
                for (int i = 0; i < 60; i++)
                {
                    Vector3 point = center;
                    NavMeshHit hit;
                    if (NavMesh.SamplePosition(point, out hit, 1.0f, 1))
                    {
                        result = hit.position;
                        return true;
                    }
                }
                result = Vector3.zero;
                return false;
            }
            if (!TentSpawnPosition(_tentPosition.position, out var point)) return;
            _navMeshAgent.Warp(point);
        }
    }
}

