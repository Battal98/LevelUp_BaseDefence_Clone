using System;
using System.Collections.Generic;
using Interfaces;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Data.ValueObject.WeaponData;
using Enums;
using Sirenix.OdinInspector;
using StateMachines.AIBrain.Soldier.States;
using UnityEngine;
using UnityEngine.AI;

namespace StateMachines.AIBrain.Soldier
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class SoldierAIBrain : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public int Health;
        public bool HasReachedSlotTarget;
        public bool HasReachedFrontYard;
        public bool HasSoldiersActivated;

        public Transform TentPosition;
        public Transform FrontYardStartPosition;
        public List<IDamageable> enemyList = new List<IDamageable>();
        public Transform EnemyTarget;
        public IDamageable DamageableEnemy;
        public Transform WeaponHolder;
        #endregion

        #region Serialized Variables

        [SerializeField] 
        private SoldierPhysicsController physicsController;
        [SerializeField] 
        private Animator animator;
        [SerializeField]
        private NavMeshAgent _navMeshAgent;
        #endregion

        #region Private Variables

        [ShowInInspector] 
        [Header("Data")]
        private SoldierAIData _data;
        private StateMachine _stateMachine;
        private Vector3 _slotTransform;

        #endregion
        #endregion
        private void Awake()
        {
            _data = GetSoldierAIData();
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
            Health = _data.Health;
        }
        private void GetStateReferences()
        {
            var idle = new IdleState(TentPosition, _navMeshAgent);
            var moveToSlotZone = new MoveToSlotState(this, _navMeshAgent, HasReachedSlotTarget, _slotTransform, animator);
            var wait = new ReachToTargetState(animator, _navMeshAgent);
            var moveToFrontYard = new MoveToFrontYardState(this, _navMeshAgent, FrontYardStartPosition, animator);
            var patrol = new SearchTargetState(this, _navMeshAgent, animator);
            var attack = new ShootTargetState(this, _navMeshAgent, animator);
            var death = new SoldierDeathState(this,animator,_navMeshAgent);

            _stateMachine = new StateMachine();

            At(idle, moveToSlotZone, hasSlotTransformList());
            At(moveToSlotZone, moveToFrontYard, hasSoldiersActivated());
            At(moveToSlotZone, wait, hasReachToSlot());
            At(wait, moveToFrontYard, hasSoldiersActivated());
            At(moveToFrontYard, patrol, hasReachedFrontYard());
            At(patrol, attack, hasEnemyTarget());
            At(attack, patrol, hasNoEnemyTarget());

            _stateMachine.SetState(idle);

            _stateMachine.AddAnyTransition(death, hasNoLife());
            void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

            Func<bool> hasSlotTransformList() => () => _slotTransform != null;
            Func<bool> hasReachToSlot() => () => _slotTransform != null && HasReachedSlotTarget;
            Func<bool> hasSoldiersActivated() => () => FrontYardStartPosition != null && HasSoldiersActivated;
            Func<bool> hasReachedFrontYard() => () => FrontYardStartPosition != null && HasReachedFrontYard;
            Func<bool> hasEnemyTarget() => () => EnemyTarget;
            Func<bool> hasNoEnemyTarget() => () => !EnemyTarget;
            Func<bool> hasNoLife() => () => Health <= 0;
        }
        private void Update() => _stateMachine.Tick();

        public void GetSlotTransform(Vector3 slotTransfrom)
        {
            _slotTransform = slotTransfrom;
        }
    }
}