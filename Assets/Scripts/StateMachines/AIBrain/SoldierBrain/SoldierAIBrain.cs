using System;
using System.Collections.Generic;
using Interfaces;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Data.ValueObject.WeaponData;
using Enums;
using Managers;
using Signals;
using Sirenix.OdinInspector;
using StateMachines;
using StateMachines.AIBrain.Soldier.States;
using UnityEngine;
using UnityEngine.AI;

namespace StateMachines.AIBrain.Soldier
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class SoldierAIBrain : MonoBehaviour, IGetPoolObject
    {
        #region Self Variables

        #region Public Variables

        public bool HasReachedSlotTarget;
        public bool HasReachedFrontYard;
        public bool HasEnemyTarget = false;

        public Transform TentPosition;
        public Transform FrontYardStartPosition;
        public List<IDamagable> enemyList = new List<IDamagable>();
        public Transform EnemyTarget;
        #endregion

        #region Serialized Variables

        [SerializeField] private SoldierPhysicsController physicsController;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform weaponHolder;
        #endregion

        #region Private Variables

        private NavMeshAgent _navMeshAgent;

        [ShowInInspector] private List<IDamagable> _damagablesList;
        [Header("Data")]
        private SoldierAIData _data;
        private int _damage;
        private float _soldierSpeed;
        private float _attackRadius;
        private Coroutine _attackCoroutine;
        private float _attackDelay;
        private int _health;
        private Transform _spawnPoint;
        private StateMachine _stateMachine;
        private Vector3 _slotTransform;
        private bool HasSoldiersActivated;
        private List<WeaponData> _weaponDatas;
        // private bool dead { get; set; }

        #endregion
        #endregion
        private void Awake()
        {
            _data = GetSoldierAIData();
            _weaponDatas = WeaponData();
            SetSoldierAIData();

        }
        private void Start()
        {
            GetStateReferences();
        }
        private SoldierAIData GetSoldierAIData() => Resources.Load<CD_WorkerAI>("Data/CD_WorkerAI").WorkerAIData.WorkerAITypes[(int)WorkerType.SoldierAI].SoldierAIData;
        private List<WeaponData> WeaponData() => Resources.Load<CD_Weapons>("Data/CD_Weapons").WeaponDatas;
        private void SetSoldierAIData()
        {
            _damage = _data.Damage;
            _soldierSpeed = _data.SoldierSpeed;
            _attackRadius = _data.AttackRadius;
            _attackCoroutine = _data.AttackCoroutine;
            _attackDelay = _data.AttackDelay;
            _health = _data.Health;
            _spawnPoint = _data.SpawnPoint;
        }
        public GameObject GetObjectType(PoolType poolName)
        {
            var bulletPrefab = PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolName);
            bulletPrefab.transform.position = weaponHolder.position;
            bulletPrefab.GetComponent<BulletPhysicsController>().soldierAIBrain = this;
            FireBullet(bulletPrefab);
            return bulletPrefab;
        }
        private void FireBullet(GameObject bulletPrefab)
        {
            bulletPrefab.transform.rotation = _navMeshAgent.transform.rotation;
            var rigidBodyBullet = bulletPrefab.GetComponent<Rigidbody>();
            rigidBodyBullet.AddForce(this.transform.forward * 40, ForceMode.VelocityChange);
        }
        private void GetStateReferences()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            var idleState = new IdleState (this, TentPosition, _navMeshAgent, animator);
            var moveToSlotZone = new MoveToSlotState (this, _navMeshAgent, HasReachedSlotTarget, _slotTransform, animator);
            var wait = new ReachToTargetState(animator, _navMeshAgent);
            var moveToFrontYard = new MoveToFrontYardState (this, _navMeshAgent, FrontYardStartPosition, animator);
            var patrol = new SearchTargetState(this, _navMeshAgent, animator);
            var attack = new ShootTargetState(this, _navMeshAgent, animator);

            _stateMachine = new StateMachine();

            At(idleState, moveToSlotZone, hasSlotTransformList());
            At(moveToSlotZone, moveToFrontYard, hasSoldiersActivated());
            At(moveToSlotZone, wait, hasReachToSlot());
            At(wait, moveToFrontYard, hasSoldiersActivated());
            At(moveToFrontYard, patrol, hasReachedFrontYard());
            At(patrol, attack, hasEnemyTarget());
            At(attack, patrol, hasNoEnemyTarget());

            _stateMachine.SetState(idleState);
            void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

            Func<bool> hasSlotTransformList() => () => _slotTransform != null;
            Func<bool> hasReachToSlot() => () => _slotTransform != null && HasReachedSlotTarget;
            Func<bool> hasSoldiersActivated() => () => FrontYardStartPosition != null && HasSoldiersActivated;
            Func<bool> hasReachedFrontYard() => () => FrontYardStartPosition != null && HasReachedFrontYard;
            Func<bool> hasEnemyTarget() => () => HasEnemyTarget;
            Func<bool> hasNoEnemyTarget() => () => !HasEnemyTarget;
        }
        private void Update() => _stateMachine.Tick();

        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            SoldierAISignals.Instance.onSoldierActivation += OnSoldierActivation;
        }
        private void UnsubscribeEvents()
        {
            SoldierAISignals.Instance.onSoldierActivation -= OnSoldierActivation;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        public void GetSlotTransform(Vector3 slotTransfrom)
        {
            _slotTransform = slotTransfrom;
        }
        private void OnSoldierActivation()
        {
            HasSoldiersActivated = true;
        }
        public void SetEnemyTargetTransform()
        {
            EnemyTarget = enemyList[0].GetTransform();
            HasEnemyTarget = true;
        }

        public void EnemyTargetStatus()
        {
            if (enemyList.Count != 0)
            {
                EnemyTarget = enemyList[0].GetTransform();
            }
            else
            {
                HasEnemyTarget = false;
            }
        }
        public void RemoveTarget()
        {
            if (enemyList.Count == 0) return;
            enemyList.RemoveAt(0);
            enemyList.TrimExcess();
            EnemyTarget = null;
            EnemyTargetStatus();
        }
    }
}