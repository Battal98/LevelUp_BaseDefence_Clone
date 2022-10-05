using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StateMachines.AIBrain.Workers.GemStates;
using StateMachines;
using Commands;
using Controllers;
using Enums;
using Managers;
using Signals;
using UnityEngine;
using UnityEngine.AI;
using Interfaces;

namespace StateMachines.AIBrain.Workers
{
    public class MinerAIBrain : MonoBehaviour
    {

        #region SelfVariables
        public Vector3 ManipulatedTarget;
        #region Public Variables
        public Transform CurrentTarget;
        public GemMineType CurrentTargetType;
        public Transform GemHolder;
        public MinerManager MinerManager;
        public float GemCollectionOffset=5;
        public bool IsDropZoneFullStatus
        {
            get => isDropZoneFull;
            set => isDropZoneFull = value;
        }

        private bool isDropZoneFull;


        

        #endregion

        #region Serialized Variables

        [SerializeField] private Animator animator;
        

        #endregion

        #region Private Variables

        private StateMachine _stateMachine;
        private FindRandomPointOnCircleCommand _findRandomPointOnCircleCommand;
        public MinerAIItemController MinerAIItemController;
        //public List<var>

        #endregion

        #endregion
        private void Awake()
        {
            _findRandomPointOnCircleCommand=new FindRandomPointOnCircleCommand();
            GetStatesReferences();
        }

        private void Start()
        {
            SetTargetForMine();
        }

        public void SetTargetForMine()
        {
            GemHolder = MineBaseSignals.Instance.onGetGemHolderPos?.Invoke();
            (CurrentTarget, CurrentTargetType)= MineBaseSignals.Instance.onGetRandomMineTarget?.Invoke();
            ManipulatedTarget= _findRandomPointOnCircleCommand.FindRandomPointOnCircle(CurrentTarget.position,3f);
        }
        public void SetTargetForGemHolder()
        {
            CurrentTarget= GemHolder;
            if (CurrentTarget.position- transform.position != Vector3.zero)
            {
                transform.forward =CurrentTarget.position- transform.position;
            }
            ManipulatedTarget= GemHolder.position+transform.TransformDirection(new Vector3(0,0,-3f));
        }

        private void GetStatesReferences()
        {
            var minerReadyState = new MinerReadyState();
            var moveToMine = new MoveState(this,MinerManager,MinerAnimationStates.Walk,MinerItems.None);
            var moveToGemHolder = new MoveState(this,MinerManager,MinerAnimationStates.CarryGem,MinerItems.Gem);
            var mineGemSourceState=new GemSourceState(this,MinerManager,MinerAnimationStates.MineGemSource,MinerItems.Pickaxe);
            var cartGemSourceState=new GemSourceState(this,MinerManager,MinerAnimationStates.CartGemSource,MinerItems.None); 
            var idleState=new MinerIdleState(this,MinerManager); 
            var dropGemState=new DropGemState(this); 

            _stateMachine = new StateMachine();

            At(minerReadyState,moveToMine,IsGameStarted());
            At(moveToMine,mineGemSourceState,()=>moveToMine.IsReachedToTarget&&CurrentTargetType==GemMineType.Mine);//su iki state tek move stat oldugu icin tekrar tekrar calisiyor
            At(moveToMine,cartGemSourceState,()=>moveToMine.IsReachedToTarget&&CurrentTargetType==GemMineType.Cart);//su iki state tek move stat oldugu icin tekrar tekrar calisiyor
            At(mineGemSourceState,moveToGemHolder,()=>mineGemSourceState.IsMiningTimeUp);
            At(cartGemSourceState,moveToGemHolder,()=>cartGemSourceState.IsMiningTimeUp);
            At(moveToGemHolder,dropGemState,()=>moveToGemHolder.IsReachedToTarget);
            At(dropGemState,moveToMine,()=>dropGemState.IsGemDropped);
            At(idleState,moveToMine,IsDropZoneNotFull());

            _stateMachine.SetState(minerReadyState);
            _stateMachine.AddAnyTransition(idleState,IsDropZoneFull());

            void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

            Func<bool> IsDropZoneFull() => () => isDropZoneFull;
            Func<bool> IsGameStarted() => () => true;
            Func<bool> IsDropZoneNotFull() => () => isDropZoneFull==false;
        }

        private void Update() => _stateMachine.Tick();
    }
}