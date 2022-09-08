using Enums;
using UnityEngine;
using Controllers;

namespace Managers
{
    public class GateManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public GateType CurrentGateState = GateType.Close;

        #endregion

        #region Serializable Variables

        [SerializeField]
        private GateAnimationControllers gateAnimationController;

        #endregion

        #endregion

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            GateSignals.Instance.onChangeGateState += OnChangeGateState;
        }

        private void UnsubscribeEvents()
        {
            GateSignals.Instance.onChangeGateState += OnChangeGateState;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void OnChangeGateState(GateType state)
        {
            CurrentGateState = state;
            gateAnimationController.ChangeAnimationState(state);
        }

        #endregion
    }


}
