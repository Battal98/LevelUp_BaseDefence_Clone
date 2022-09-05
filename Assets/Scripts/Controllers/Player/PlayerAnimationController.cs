using Enums;
using UnityEngine;

namespace Controllers
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Animator animatorController;

        #endregion

        #endregion

        public void PlayAnim(float Value)
        {
            animatorController.SetFloat("Run", Value);

        }

        public void ChangePlayerAnimation(PlayerAnimationStates state, bool value)
        {
            animatorController.SetBool(state.ToString(), value);
        }



    }
}