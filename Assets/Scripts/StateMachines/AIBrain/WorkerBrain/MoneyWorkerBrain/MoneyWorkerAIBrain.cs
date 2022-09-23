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

        private WorkerAIData _workerAIData;
        [ShowInInspector]
        private WorkerAITypeData _workerTypeData;
        private Animator _animator;
        private NavMeshAgent _navmeshAgent;

        #region States

        private MoveToGateState _moveToGateState;
        private WaitOnGateState _waitOnGateState;
        private StackMoneyState _stackMoneyState;
        private DropMoneyOnGateState _dropMoneyOnGateState;
        private StateMachine _stateMachine;

        #endregion

        #region Worker Game Variables

        private int _currentStock = 0;
        private int _totalMoneyCapacity;
        private float _speed;
        private Transform _target;

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

        #region Data Jobs

        private WorkerAITypeData GetWorkerType()
        {
            return WorkerSignals.Instance.onGetMoneyAIData?.Invoke(workerType);
        }

        private void SetWorkerComponentVariables()
        {
            _navmeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponentInChildren<Animator>();
        }

        #endregion

        private void InitWorker()
        {

        }

        #region Worker State Jobs

        private void GetReferenceStates()
        {
            _moveToGateState = new MoveToGateState(_navmeshAgent, _animator, ref _currentStock  ,ref _workerTypeData.CapacityOrDamage, ref _workerTypeData.Speed ,ref _workerTypeData.StartTarget);
            _waitOnGateState = new WaitOnGateState();
            _stackMoneyState = new StackMoneyState(_navmeshAgent, _animator, ref _currentStock, ref _workerTypeData.CapacityOrDamage, ref _workerTypeData.Speed,this);
            _dropMoneyOnGateState = new DropMoneyOnGateState();

            _stateMachine = new StateMachine();

            At(_moveToGateState, _waitOnGateState, HasNoTarget());
            At(_waitOnGateState, _stackMoneyState, HasCurrentTargetMoney());
            At(_stackMoneyState, _dropMoneyOnGateState, HasCapacityFull());

            _stateMachine.SetState(_moveToGateState);
            void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

            Func<bool> HasNoTarget() => () => CurrentTarget == null || _moveToGateState.IsArrive;
            Func<bool> HasCurrentTargetMoney() => () => CurrentTarget != null  ;
            Func<bool> HasCapacityFull() => () => _currentStock == _totalMoneyCapacity;
        }

        private void Update() => _stateMachine.Tick();


        #endregion


    }
}
