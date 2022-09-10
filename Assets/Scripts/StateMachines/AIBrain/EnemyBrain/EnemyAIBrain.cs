using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Interfaces;
using StateMachines.AIBrain.Enemy.States;
using System;
using Data.ValueObject.AIDatas;
using Sirenix.OdinInspector;
using Enums;

namespace StateMachines.AIBrain.Enemy
{
    public class EnemyAIBrain : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables 

        [BoxGroup("Targets")]
        public Transform PlayerTarget;
        [BoxGroup("Targets")]
        public List<Transform> TurretTargetList;

        #endregion

        #region Serilizable Variables

        [BoxGroup("Serializable Variables")]
        [SerializeField]
        private EnemyType enemyType;

        [BoxGroup("Serializable Variables")]
        [SerializeField]
        private EnemyPhysicController detector;

        #endregion

        #region Private Variables

        [ShowInInspector]
        private EnemyTypeData EnemyTypeDatas;
        private StateMachine _stateMachine;
        private NavMeshAgent _navmeshAgent;
        private Animator _animator;

        #region States

        private MoveState _moveState;
        private ChaseState _chaseState;
        private AttackState _attackState;
        private MoveToBombState _moveToBombState;
        private DeathState _deathState;

        #endregion

        #region Enemy Game Variables

        private int _health;
        private int _damage;
        private float _attackRange;
        private float _attackSpeed;
        private float _moveSpeed;
        private float _chaseSpeed;
        private Color _myColor;

        #endregion

        #endregion

        #endregion

        private void Awake()
        {
            EnemyTypeDatas = GetData();
            SetEnemyVariables();
            GetReferenceStates();
            InitEnemy();
        }

        #region Data Jobs
        private EnemyTypeData GetData()
        {
            return Resources.Load<CD_EnemyAI>("Data/CD_EnemyAI").EnemyAIData.EnemyList[(int)enemyType];
        }

        private void SetEnemyVariables()
        {
            _health = EnemyTypeDatas.Health;
            _damage = EnemyTypeDatas.Damage;
            _attackRange = EnemyTypeDatas.AttackRange;
            _attackSpeed = EnemyTypeDatas.AttackSpeed;
            _chaseSpeed = EnemyTypeDatas.ChaseSpeed;
            _moveSpeed = EnemyTypeDatas.MoveSpeed;
            _myColor = EnemyTypeDatas.BodyColor;
        }

        private void InitEnemy()
        {
            //mesh controller olusturulabilir
            this.GetComponentInChildren<SkinnedMeshRenderer>().material.color = _myColor;
        }
        #endregion

        #region AI State Jobs
        private void GetReferenceStates()
        {
            _navmeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();

            _moveState = new MoveState(_navmeshAgent, _animator, this, _moveSpeed);
            _chaseState = new ChaseState(_navmeshAgent, _animator, this, _attackRange, _chaseSpeed);
            _attackState = new AttackState(_navmeshAgent, _animator, this, _attackRange);
            _moveToBombState = new MoveToBombState(_navmeshAgent, _animator);
            _deathState = new DeathState(_navmeshAgent, _animator);

            //Statemachine statelerden sonra tanýmlanmalý ?
            _stateMachine = new StateMachine();

            At(_moveState, _chaseState, HasTarget()); // playerinrange
            At(_chaseState, _attackState, AmIAttackPlayer()); // remaining distance<1f and playerinattackrange
            At(_chaseState, _moveState, HasNoTarget());
            At(_attackState, _chaseState, () => _attackState.InPlayerAttackRange() == false); // remaining distance> 1f// remaining distance> 1f

            _stateMachine.AddAnyTransition(_deathState, _deathState.AmIDead);
            _stateMachine.AddAnyTransition(_moveToBombState, detector.IsBombInRange);
            At(_moveToBombState, _attackState, AmIAttackBomb());

            //SetState state durumlarý belirlendikten sonra default deger cagýrilmali
            _stateMachine.SetState(_moveState);

            void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

            Func<bool> HasTarget() => () => PlayerTarget != null;
            Func<bool> HasNoTarget() => () => PlayerTarget == null;
            Func<bool> AmIAttackPlayer() => () => _chaseState.InPlayerAttackRange() && PlayerTarget != null;
            Func<bool> AmIAttackBomb() => () => detector.IsBombInRange() &&
                                                        PlayerTarget == null;
            //Func<bool> AmIStuck() => () => _moveState.TimeStuck > 1f;

        }

        #endregion

        private void Update() => _stateMachine.Tick();

    } 
}
