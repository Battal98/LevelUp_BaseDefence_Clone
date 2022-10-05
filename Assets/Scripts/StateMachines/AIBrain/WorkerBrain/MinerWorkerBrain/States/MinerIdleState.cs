using Enums;
using Managers;
using UnityEngine;
using Interfaces;

namespace StateMachines.AIBrain.Workers.GemStates
{
    public class MinerIdleState:IState

    {
        private MinerManager _minerManager;
        private MinerAIBrain _minerAIBrain;
        public MinerIdleState(MinerAIBrain minerAIBrain, MinerManager minerManager)
        {
            _minerAIBrain = minerAIBrain;
            _minerManager = minerManager;
        }

        public void Tick()
        {
            
        }

        public void OnEnter()
        {
            _minerAIBrain.SetTargetForMine();
            _minerManager.ChangeAnimation(MinerAnimationStates.Idle);
        }

        public void OnExit()
        {
            
        }
    }
}