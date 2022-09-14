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

        public void OnChangePlayerAnimationState(PlayerAnimationStates states)
        {
            if (State != states)
            {
                State = states;
                playerAnimatorController.SetTrigger(states.ToString());
            }
        }

    }
}