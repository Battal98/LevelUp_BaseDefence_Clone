using Enums;
using UnityEngine;

namespace Controllers
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] 
        private Animator playerAnimatorController;
        [SerializeField]
        private PlayerAnimationStates State = PlayerAnimationStates.Idle;

        #endregion

        #endregion

        public void SetPlayerAnimation(PlayerAnimationStates states)
        {
            if (State != states)
            {
                State = states;
                playerAnimatorController.SetTrigger(states.ToString());
                Debug.Log(states.ToString());
            }
        }

        public void OnChangePlayerAnimationState(PlayerAnimationStates states)
        {
            switch (states)
            {
                case PlayerAnimationStates.Idle:
                    SetPlayerAnimation(PlayerAnimationStates.Idle);
                    break;
                case PlayerAnimationStates.Run:
                    SetPlayerAnimation(PlayerAnimationStates.Run);
                    break;
                case PlayerAnimationStates.Dead:
                    SetPlayerAnimation(PlayerAnimationStates.Dead);
                    break;
                default:
                    break;
            }
        }


    }
}