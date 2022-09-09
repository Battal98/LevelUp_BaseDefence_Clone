using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Interfaces;
using StateMachines.AIBrain.Enemy.States;
using System;

namespace StateMachines.AIBrain.Enemy
{
    public class EnemyAIBrain : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables 

        public Transform Target;

        #endregion

        #region Serilizable Variables

        [SerializeField]
        private Detector detector;

        #endregion

        #region Private Variables

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

        #endregion

        #endregion

        private void Awake()
        {
            GetReferencesDefault();
            GetReferenceStates();
            SetTransition();
        }

        private void GetReferenceStates()
        {
            _moveState = new MoveState(_navmeshAgent, _animator);
            _chaseState = new ChaseState(_navmeshAgent, _animator);
            _attackState = new AttackState(_navmeshAgent, _animator);
            _moveToBombState = new MoveToBombState(_navmeshAgent, _animator);
            _deathState = new DeathState(_navmeshAgent, _animator);

            _stateMachine.SetState(_moveState);

        }

        private void GetReferencesDefault()
        {
            _stateMachine = new StateMachine();
            _navmeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }

        private void SetTransition()
        {
            At(_moveState, _chaseState,HasTarget()); // playerinrange
            At(_chaseState, _attackState,AmIAttackPlayer()); // remaining distance<1f and playerinattackrange
            At(_attackState, _chaseState, () => detector.IsPlayerInRange() == false); // remaining distance> 1f

            _stateMachine.AddAnyTransition(_deathState, _deathState.AmIDead);
            _stateMachine.AddAnyTransition(_moveToBombState, detector.IsBombInRange);
            At(_moveToBombState, _attackState, AmIAttackBomb());

            Func<bool> HasTarget() => () => Target != null;
            Func<bool> AmIAttackPlayer() => () => detector.IsPlayerInRange() &&
                                                        Target != null;
            Func<bool> AmIAttackBomb() => () => detector.IsBombInRange() &&
                                                        Target != null;
            //Func<bool> AmIStuck() => () => _moveState.TimeStuck > 1f;
        }

        private void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

        private void Update() => _stateMachine.Tick();


    } 
}
