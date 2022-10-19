using UnityEngine;
using UnityEngine.AI;
using Interfaces;
using Signals;
using Enums;
using DG.Tweening;

namespace StateMachines.AIBrain.Soldier.States
{
    public class SoldierDeathState : IState, IReleasePoolObject
    {
        private Animator _soldierAnimator;
        private SoldierAIBrain _soldierAIBrain;
        private NavMeshAgent _navMeshAgent;
        private PoolType poolType;
        public SoldierDeathState(SoldierAIBrain soldierAIBrain, Animator soldierAnimator, NavMeshAgent navMeshAgent)
        {
            _soldierAIBrain = soldierAIBrain;
            _soldierAnimator = soldierAnimator;
            _navMeshAgent = navMeshAgent;
            poolType = PoolType.SoldierAI;
        }
        public void Tick()
        {

        }
        public void OnEnter()
        {
            SoldierDead();
            _soldierAnimator.SetTrigger("Death");
        }
        public void OnExit()
        {

        }
        private void SoldierDead()
        {
            var poolType = (PoolType)System.Enum.Parse(typeof(PoolType), this.poolType.ToString());
            DOVirtual.DelayedCall(1.5f, () =>
            {
                _soldierAIBrain.transform.DOMoveY(-3f, 1f).OnComplete(() => ReleaseObject(_soldierAIBrain.gameObject, poolType));
            });
        }
        public void ReleaseObject(GameObject obj, PoolType poolName)
        {
            PoolSignals.Instance.onReleaseObjectFromPool?.Invoke(poolName, obj);
        }
    }
}
