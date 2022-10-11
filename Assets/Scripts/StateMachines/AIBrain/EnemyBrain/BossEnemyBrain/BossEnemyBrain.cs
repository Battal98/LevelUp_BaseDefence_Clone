using UnityEngine;
using Interfaces;
using StateMachines.AIBrain.Enemy.States;
using System;
using Data.ValueObject.AIDatas;
using Sirenix.OdinInspector;
using Enums;
using Controllers;

namespace StateMachines.AIBrain.Enemy
{
    public class BossEnemyBrain : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables 

        [BoxGroup("Public Variables")]
        public Transform PlayerTarget;
        [BoxGroup("Public Variables")]
        public int Health;
        #endregion

        #region Serilizable Variables

        [BoxGroup("Serializable Variables")]
        [SerializeField]
        private EnemyType enemyType;

        [BoxGroup("Serializable Variables")]
        [SerializeField]
        private BossEnemyDetector detector;

        [BoxGroup("Serializable Variables")]
        [SerializeField]
        private Transform bombHolder;


        #endregion

        #region Private Variables
        private EnemyTypeData _enemyTypeData;
        private EnemyAIData _enemyAIData;
        private StateMachine _stateMachine;
        private Animator _animator;

        #region States

        private BossWaitState _waitState;
        private BossAttackState _attackState;
        private BossDeathState _deathState;

        #endregion

        #endregion

        #endregion

        private void Awake()
        {
            _enemyAIData = GetAIData();
            _enemyTypeData = GetEnemyType();
            SetEnemyVariables(); 
            GetReferenceStates();
        }

        private void SetEnemyVariables()
        {
            _animator = GetComponentInChildren<Animator>();
            Health = _enemyTypeData.Health;
        }

        #region Data Jobs
        private EnemyTypeData GetEnemyType()
        {
            return _enemyAIData.EnemyList[(int)enemyType];
        }
        private EnemyAIData GetAIData()
        {
            return Resources.Load<CD_EnemyAI>("Data/CD_EnemyAI").EnemyAIData;
        }
        #endregion

        #region AI State Jobs
        private void GetReferenceStates()
        {

            _waitState = new BossWaitState(_animator, this);
            _attackState = new BossAttackState( _animator, this, _enemyTypeData.AttackRange, ref bombHolder);
            _deathState = new BossDeathState( _animator, this, enemyType);

            //Statemachine statelerden sonra tanimlanmali ?
            _stateMachine = new StateMachine();

            At(_waitState, _attackState, IAttackPlayer()); 
            At(_attackState, _waitState, INoAttackPlayer()); 

            _stateMachine.AddAnyTransition(_deathState, AmIDead());
            //SetState state durumlari belirlendikten sonra default deger cagirilmali
            _stateMachine.SetState(_waitState);

            void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
            Func<bool> IAttackPlayer() => () => PlayerTarget != null;
            Func<bool> INoAttackPlayer() => () => PlayerTarget == null;
            Func<bool> AmIDead() => () => Health <= 0;

        }

        #endregion

        private void Update() => _stateMachine.Tick();

        [Button]
        private void BossDeathState()
        {
            Health = 0;
        } 
    }
}
