using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using StateMachines.AIBrain.Workers.MoneyStates;
using Sirenix.OdinInspector;
using Enums;
using Controllers;
using Data.UnityObject;
using Data.ValueObject.AIDatas;
using Interfaces;
using System;
using Signals;

namespace StateMachines.AIBrain.Workers
{
    public class MoneyWorkerAIBrain : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [BoxGroup("Public Variables")]
        public Transform CurrentTarget;

        #endregion

        #region Serilizable Variables

        [BoxGroup("Serializable Variables")]
        [SerializeField]
        private WorkerType workerType;
        [BoxGroup("Serializable Variables")]
        [SerializeField]
        private MoneyWorkerPhysicController moneyWorkerDetector;

        #endregion

        #region Private Variables

        [ShowInInspector]
        private WorkerAITypeData _workerTypeData;
        private Animator _animator;
        private NavMeshAgent _navmeshAgent;

        #region States

        private MoveToGateState _moveToGateState;
        private SearchState _searchState;
        private WaitOnGateState _waitOnGateState;
        private StackMoneyState _stackMoneyState;
        private DropMoneyOnGateState _dropMoneyOnGateState;
        private StateMachine _stateMachine;

        #endregion

        #region Worker Game Variables

        private int _currentStock = 0;

        #endregion

        #endregion

        #endregion

        private void Awake()
        {
            _workerTypeData = GetWorkerType();
            SetWorkerComponentVariables();
            InitWorker();
            GetReferenceStates();
        }

        /*#region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            EnemySignals.Instance.onEnemyDead += OnGetEnemyPositon;
        }
        private void UnsubscribeEvents()
        {
            EnemySignals.Instance.onEnemyDead -= OnGetEnemyPositon;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        private void OnGetEnemyPositon(Transform pos)
        {
            moneyTargetList.Add(pos);
        }

        #endregion*/


        #region Data Jobs

        private WorkerAITypeData GetWorkerType()
        {
            return MoneyWorkerSignals.Instance.onGetMoneyAIData?.Invoke(workerType);
        }

        private void SetWorkerComponentVariables()
        {
            _navmeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponentInChildren<Animator>();
        }

        public int SetCurrentStock()
        {
            return _currentStock;
        }

        public bool IsAvaiable() => CurrentTarget == null && _currentStock < _workerTypeData.CapacityOrDamage;

        #endregion

        private void InitWorker()
        {

        }

        public Transform GetMoneyPosition()
        {
            return MoneyWorkerSignals.Instance.onGetTransformMoney?.Invoke(this.transform);
        }

        #region Worker State Jobs

        private void GetReferenceStates()
        {
            _searchState = new SearchState(_navmeshAgent, _animator, ref _currentStock, ref _workerTypeData.CapacityOrDamage, this);
            _moveToGateState = new MoveToGateState(_navmeshAgent, _animator, ref _currentStock  ,ref _workerTypeData.CapacityOrDamage, ref _workerTypeData.Speed ,ref _workerTypeData.StartTarget);
            _waitOnGateState = new WaitOnGateState(_navmeshAgent, _animator, this);
            _stackMoneyState = new StackMoneyState(_navmeshAgent, _animator,this);
            _dropMoneyOnGateState = new DropMoneyOnGateState();

            _stateMachine = new StateMachine();

            At(_moveToGateState, _waitOnGateState, HasNoTarget());
            At(_waitOnGateState, _stackMoneyState, HasCurrentTargetMoney());
            At(_stackMoneyState, _searchState, _stackMoneyState.IsArriveToMoney());
            At(_stackMoneyState, _dropMoneyOnGateState, HasCapacityFull());

            _stateMachine.SetState(_moveToGateState);
            void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

            Func<bool> HasNoTarget() => () => CurrentTarget != null || _moveToGateState.IsArrive;
            Func<bool> HasCurrentTargetMoney() => () => CurrentTarget != null;
            Func<bool> HasCapacityFull() => () => _currentStock == _workerTypeData.CapacityOrDamage;
        }

        private void Update() => _stateMachine.Tick();


        #endregion


    }
}
