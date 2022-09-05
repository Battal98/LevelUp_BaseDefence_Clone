using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Keys;
using Signals;
using TMPro;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public PlayerData Data;

        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerAnimationController playerAnimationController;
        [SerializeField] private PlayerMovementController playerMovementController;
        [SerializeField] private TextMeshPro scoreText;

        #endregion

        #region Private Variables

        //private int _score;
        [ShowInInspector]
        private PlayerAnimationStates _animationState;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
            SendPlayerDataToControllers();
        }

        private void GetReferences()
        {
            Data = GetPlayerData();
        }

        #region Event Subscription

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            InputSignals.Instance.onInputTaken += playerMovementController.EnableMovement;
            InputSignals.Instance.onInputReleased += playerMovementController.DeactiveMovement;
            InputSignals.Instance.onInputDragged += OnInputDragged;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            LevelSignals.Instance.onLevelFailed += OnLevelFailed;
            
            //ScoreSignals.Instance.onSetPlayerScore += OnSetScore;
        }


        private void Unsubscribe()
        {
            InputSignals.Instance.onInputTaken -= playerMovementController.EnableMovement;
            InputSignals.Instance.onInputReleased -= playerMovementController.DeactiveMovement;
            InputSignals.Instance.onInputDragged -= OnInputDragged;
            LevelSignals.Instance.onLevelFailed -= OnLevelFailed;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;

           // ScoreSignals.Instance.onSetPlayerScore -= OnSetScore;
        }


        private void OnDisable()
        {
            Unsubscribe();
        }

        #endregion

        private PlayerData GetPlayerData()
        {
            return Resources.Load<CD_Player>("Data/CD_Player").Data;
        }


        private void SendPlayerDataToControllers()
        {
            playerMovementController.SetMovementData(Data.MovementData);
            playerMovementController.IsReadyToPlay(true);
        }

        private void OnSetScore(int values)
        {
            //_score = values;
            SetScoreText(values);
        }

        private void SetScoreText(int values)
        {
            scoreText.text = values.ToString();
        }


        private void OnInputDragged(InputParams InputParam)
        {
            playerMovementController.UpdateInputValue(InputParam);
            PlayAnim(Mathf.Abs(InputParam.Values.x + InputParam.Values.y));
        }

        private void PlayAnim(float value)
        {
            playerAnimationController.PlayAnim(value);
        }

        private void ChangePlayerAnim()
        {
            playerAnimationController.ChangePlayerAnimation(_animationState, true);
        }

        private void OnLevelFailed()
        {
            playerMovementController.IsReadyToPlay(false);
        }


        private void OnPlay()
        {
            //playerMovementController.IsReadyToPlay(true);
        }

        private void OnReset()
        {
            playerMovementController.OnReset();
        }
    }
}