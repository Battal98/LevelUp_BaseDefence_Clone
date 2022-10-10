using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using Enums;

namespace StateMachines.AIBrain.Enemy.States
{
    public class BossDeathState : IState
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
            Debug.Log("Boss Death Enter");
            _animator.SetTrigger("Death");
        }

        public void OnExit()
        {
            Debug.Log("Boss Death Exit");
        }

        public void Tick()
        {
            
        }

    } 
}
