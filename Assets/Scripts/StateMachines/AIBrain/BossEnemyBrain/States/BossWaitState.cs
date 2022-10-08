using UnityEngine;
using Interfaces;

namespace StateMachines.AIBrain.Enemy.States
{
    public class BossWaitState : IState
    {
        #region Self Variables

        #region Private Variables

        private BossEnemyBrain _bossEnemyBrain;
        private Animator _animator;

    #endregion

    #endregion

        public BossWaitState(Animator animator, BossEnemyBrain bossEnemyBrain)
        {
            _bossEnemyBrain = bossEnemyBrain;
            _animator = animator;
        }
        public void OnEnter()
        {
            Debug.Log("Waiting Boss");
            _animator.SetTrigger("Idle");
        }

        public void OnExit()
        {
            
        }

        public void Tick()
        {
            
        }
    } 
}
