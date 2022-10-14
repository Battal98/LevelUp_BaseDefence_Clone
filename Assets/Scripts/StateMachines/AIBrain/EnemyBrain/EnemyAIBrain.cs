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
using Controllers;

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

        public int Health;
        #endregion

        #region Serilizable Variables

        [BoxGroup("Serializable Variables")]
        [SerializeField]
        private EnemyType enemyType;

        [BoxGroup("Serializable Variables")]
        [SerializeField]
        private EnemyDetectionController detector;

        #endregion

        #region Private Variables

        [ShowInInspector]
        private EnemyTypeData _enemyTypeData;
        private EnemyAIData _enemyAIData;
        private StateMachine _stateMachine;
        private Animator _animator;
        private NavMeshAgent _navmeshAgent;

        #region States

        private BirthState _birthState;
        private MoveState _moveState;
        private ChaseState _chaseState;
        private AttackState _attackState;
        private MoveToBombState _moveToBombState;
        private DeathState _deathState;
        private BaseAttackState _baseAttackState;

        #endregion

        #region Enemy Game Variables

        private Transform _spawnPoint;
        [ShowInInspector]
        private Transform _turretTarget;

        #endregion

        #endregion

        #endregion

        private void Awake()
        {
            _spawnPoint = EnemySignals.Instance.onGetSpawnTransform?.Invoke();
            _turretTarget = EnemySignals.Instance.onGetTargetTransform?.Invoke();
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
            //datadan health'ý tekrar çekmen gerekebilir
            Health = _enemyTypeData.Health;
        }

        private void InitEnemy()
        {
            //mesh controller olusturulabilir
            this.GetComponentInChildren<SkinnedMeshRenderer>().material.color = _enemyTypeData.BodyColor;
            //
            this.transform.localScale = _enemyTypeData.ScaleSize;
            _navmeshAgent.height = _enemyTypeData.NavMeshHeight;
            _navmeshAgent.radius = _enemyTypeData.NavMeshRadius;
        }
        #endregion

        #region AI State Jobs
        private void GetReferenceStates()
        {

            _birthState = new BirthState(_navmeshAgent,_animator,this,_spawnPoint); 
            _moveState = new MoveState(_navmeshAgent, _animator, this, _enemyTypeData.MoveSpeed, ref _turretTarget);
            _chaseState = new ChaseState(_navmeshAgent, _animator, this, _enemyTypeData.AttackRange, _enemyTypeData.ChaseSpeed);
            _attackState = new AttackState(_navmeshAgent, _animator, this, _enemyTypeData.AttackRange);
            _moveToBombState = new MoveToBombState(_navmeshAgent, _animator);
            _deathState = new DeathState(_navmeshAgent, _animator, this, enemyType.ToString());
            _baseAttackState = new BaseAttackState(_navmeshAgent, _animator);


            //Statemachine statelerden sonra tanimlanmali ?
            _stateMachine = new StateMachine();

            At(_birthState, _moveState, HasTargetTurret());
            At(_moveState, _chaseState, HasTargetPlayer()); // playerinrange
            At(_chaseState, _attackState, IAttackPlayer()); // remaining distance<1f and playerinattackrange
            At(_chaseState, _moveState, HasNoTargetPlayer());
            At(_attackState, _chaseState, INoAttackPlayer()); // remaining distance> 1f// remaining distance> 1f

            At(_moveState, _baseAttackState, IsEnemyReachedBase());
            At(_baseAttackState, _chaseState, HasTargetPlayer());


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
            Func<bool> AmIDead() => () => Health <= 0;

            Func<bool> IsEnemyReachedBase() => () => _navmeshAgent.remainingDistance <= _enemyTypeData.AttackRange;

            /*Func<bool> AmIAttackBomb() => () => detector.IsBombInRange() &&
                                                        PlayerTarget == null;*/
            //Func<bool> AmIStuck() => () => _moveState.TimeStuck > 1f;

        }

        #endregion

        private void Update() => _stateMachine.Tick();

    } 
}
