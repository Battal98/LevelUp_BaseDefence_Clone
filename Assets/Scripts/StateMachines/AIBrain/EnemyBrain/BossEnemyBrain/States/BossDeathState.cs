using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using Enums;
using Signals;

namespace StateMachines.AIBrain.Enemy.States
{
    public class BossDeathState : IState, IReleasePoolObject
    {
        #region Self Variables

        #region Private Variables

        private readonly BossEnemyBrain _bossEnemyBrain;
        private readonly Animator _animator;
        private readonly EnemyType _enemyType;

        #endregion

        #endregion

        public BossDeathState(Animator animator, BossEnemyBrain bossEnemyBrain, EnemyType enemyType)
        {
            _bossEnemyBrain = bossEnemyBrain;
            _animator = animator;
            _enemyType = enemyType;
        }
        public void OnEnter()
        {
            _animator.SetTrigger("Death");
            EnemySignals.Instance.onOpenPortal?.Invoke();
            ReleaseObject(_bossEnemyBrain.gameObject, PoolType.Boss);
            //Level Completed
        }

        public void OnExit()
        {
            
        }

        public void ReleaseObject(GameObject obj, PoolType poolType)
        {
            PoolSignals.Instance.onReleaseObjectFromPool?.Invoke(poolType, obj);
        }

        public void Tick()
        {
            
        }

    } 
}
