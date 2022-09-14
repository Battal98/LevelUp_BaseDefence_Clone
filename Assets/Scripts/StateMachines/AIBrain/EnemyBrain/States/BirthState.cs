using UnityEngine;
using UnityEngine.AI;
using Interfaces;

namespace StateMachines.AIBrain.Enemy.States
{
    public class BirthState : IState
    {
        private readonly EnemyAIBrain _enemyAIBrain;
        private readonly NavMeshAgent _navmeshAgent;
        private readonly Animator _animator;
        private readonly Transform _spawnPoint;

        public BirthState(NavMeshAgent navMeshAgent, Animator animator, EnemyAIBrain enemyAIBrain, Transform spawnPoint)
        {
            _navmeshAgent = navMeshAgent;
            _animator = animator;
            _spawnPoint = spawnPoint;
            _enemyAIBrain = enemyAIBrain;
        }
        public void OnEnter()
        {
            _navmeshAgent.enabled = true;
            GetRandomPointOnBakedSurface();
        }

        public void OnExit()
        {
            
        }

        public void Tick()
        {
            
        }

        private void GetRandomPointOnBakedSurface()
        {
            bool RandomPoint(Vector3 center, float range, out Vector3 result)
            {
                for (int i = 0; i < 60; i++)
                {
                    Vector3 randomPoint = center + Random.insideUnitSphere * range;
                    Vector3 randomPos = new Vector3(randomPoint.x, 0, _spawnPoint.transform.position.z);
                    NavMeshHit hit;
                    if (NavMesh.SamplePosition(randomPos, out hit, 1.0f, 1))
                    {
                        result = hit.position;
                        return true;
                    }
                }
                result = Vector3.zero;
                return false;
            }

            Vector3 point;
            if (!RandomPoint(_spawnPoint.transform.position, 20, out point)) return;
            _navmeshAgent.Warp(point);

        }
    }
}
