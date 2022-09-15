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
using Managers;
using Signals;

namespace StateMachines.AIBrain.Enemy
{
    public class EnemyAIBrain : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables 

        [BoxGroup("Targets")]
        public Transform PlayerTarget;
        [BoxGroup("Targets")]
        public Transform MineTarget;

        public NavMeshAgent _navmeshAgent;
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
        private EnemyTypeData _enemyTypeData;
        private EnemyAIData _enemyAIData;
        private StateMachine _stateMachine;
        private Animator _animator;
        private int _levelID;

        #region States

        private BirthState _birthState;
        private MoveState _moveState;
        private ChaseState _chaseState;
        private AttackState _attackState;
        private MoveToBombState _moveToBombState;
        private DeathState _deathState;

        #endregion

        #region Enemy Game Variables

        public int _health;
        private int _damage;
        private float _attackRange;
        private float _attackSpeed;
        private float _moveSpeed;
        private float _chaseSpeed;
        private Color _myColor;
        private Vector3 _scaleSize;
        private float _navmeshRadius;
        private float _navmeshHeight;
        private Transform _spawnPoint;
        [ShowInInspector]
        private Transform _turretTarget;

        #endregion

        #endregion

        #endregion

        private void Awake()
        {
            _levelID = LevelSignals.Instance.onGetLevel();
            _enemyAIData = GetAIData();
            _enemyTypeData = GetEnemyType();
            SetEnemyVariables();
            InitEnemy();
            GetReferenceStates();
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

        private void SetEnemyVariables()
        {
            _navmeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponentInChildren<Animator>();

            _health = _enemyTypeData.Health;
            _damage = _enemyTypeData.Damage;
            _attackRange = _enemyTypeData.AttackRange;
            _attackSpeed = _enemyTypeData.AttackSpeed;
            _chaseSpeed = _enemyTypeData.ChaseSpeed;
            _moveSpeed = _enemyTypeData.MoveSpeed;
            _myColor = _enemyTypeData.BodyColor;
            _scaleSize = _enemyTypeData.ScaleSize;
            _navmeshRadius = _enemyTypeData.NavMeshRadius;
            _navmeshHeight = _enemyTypeData.NavMeshHeight;
            _spawnPoint = _enemyAIData.SpawnPosList[_levelID];
            _turretTarget = _enemyAIData.SpawnPosList[_levelID].GetChild(UnityEngine.Random.Range(0, _enemyAIData.SpawnPosList[_levelID].childCount)) ;
        }

        private void InitEnemy()
        {
            //mesh controller olusturulabilir
            this.GetComponentInChildren<SkinnedMeshRenderer>().material.color = _myColor;
            //
            this.transform.localScale = _scaleSize;
            _navmeshAgent.height = _navmeshHeight;
            _navmeshAgent.radius = _navmeshRadius;
        }
        #endregion

        #region AI State Jobs
        private void GetReferenceStates()
        {

            _birthState = new BirthState(_navmeshAgent,_animator,this,_spawnPoint); 
            _moveState = new MoveState(_navmeshAgent, _animator, this, _moveSpeed, ref _turretTarget);
            _chaseState = new ChaseState(_navmeshAgent, _animator, this, _attackRange, _chaseSpeed);
            _attackState = new AttackState(_navmeshAgent, _animator, this, _attackRange);
            _moveToBombState = new MoveToBombState(_navmeshAgent, _animator);
            _deathState = new DeathState(_navmeshAgent, _animator);

            //Statemachine statelerden sonra tanimlanmali ?
            _stateMachine = new StateMachine();

            At(_birthState, _moveState, HasTargetTurret());
            At(_moveState, _chaseState, HasTargetPlayer()); // playerinrange
            At(_chaseState, _attackState, IAttackPlayer()); // remaining distance<1f and playerinattackrange
            At(_chaseState, _moveState, HasNoTargetPlayer());
            At(_attackState, _chaseState, INoAttackPlayer()); // remaining distance> 1f// remaining distance> 1f

            _stateMachine.AddAnyTransition(_deathState, AmIDead());
            _stateMachine.AddAnyTransition(_moveToBombState, detector.IsBombInRange);
            //At(_moveToBombState, _attackState, AmIAttackBomb());

            //SetState state durumlari belirlendikten sonra default deger cagirilmali
            _stateMachine.SetState(_birthState);

            void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
            Func<bool> HasTargetTurret() => () => _turretTarget != null /*&& TurretTarget.TryGetComponent(out TurretManager turret)*/;
            Func<bool> HasTargetPlayer() => () => PlayerTarget != null && PlayerTarget.TryGetComponent(out PlayerManager player);
            Func<bool> HasNoTargetPlayer() => () => PlayerTarget == null;
            Func<bool> IAttackPlayer() => () => _chaseState.InPlayerAttackRange() && PlayerTarget != null;
            Func<bool> INoAttackPlayer() => () => _attackState.InPlayerAttackRange() == false || PlayerTarget == null;
            Func<bool> AmIDead() => () => _health <= 0;
            /*Func<bool> AmIAttackBomb() => () => detector.IsBombInRange() &&
                                                        PlayerTarget == null;*/
            //Func<bool> AmIStuck() => () => _moveState.TimeStuck > 1f;

        }

        #endregion

        private void Update() => _stateMachine.Tick();

    } 
}
