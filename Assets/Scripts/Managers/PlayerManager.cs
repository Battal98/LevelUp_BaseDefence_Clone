using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Data.ValueObject.WeaponData;
using Enums;
using Keys;
using Signals;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using System;
using DG.Tweening;
using Enums.Player;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public AreaTypes CurrentAreaType = AreaTypes.BaseDefense;

        public WeaponType WeaponType;

        public List<IDamageable> EnemyList = new List<IDamageable>();

        public Transform EnemyTarget;

        public IDamageable Damageable;

        public int Health = 100;

        #endregion

        #region Serialized Variables

        [SerializeField]
        private PlayerMeshController meshController;

        [SerializeField]
        private PlayerAnimationController animationController;

        [SerializeField]
        private PlayerWeaponController weaponController;

        [SerializeField]
        private PlayerShootingController shootingController;

        [SerializeField]
        private PlayerMovementController movementController;

        [SerializeField]
        private PlayerHealthController playerHealthController;

        [SerializeField]
        private PlayerWalletController playerWalletController;

        [SerializeField]
        private StackerController playerMoneyStackerController;

        [SerializeField]
        private PlayerPhysicsController playerPhysicsController;
        #endregion

        #region Private Variables

        private PlayerData _data;

        private WeaponData _weaponData;

        #endregion

        #endregion
        private void Awake()
        {
            _data = GetPlayerData();
            _weaponData = GetWeaponData();
            Init();
            CoreGameSignals.Instance.onSetCameraTarget(transform);
        }
        private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_Player").PlayerData;
        private WeaponData GetWeaponData() => Resources.Load<CD_Weapons>("Data/CD_Weapons").WeaponDatas[(int)WeaponType];
        private void Init() => SetDataToControllers();
        private void SetDataToControllers()
        {
            movementController.SetMovementData(_data.PlayerMovementData);
            weaponController.SetWeaponData(_weaponData);
            meshController.SetWeaponData(_weaponData);
            playerHealthController.SetHealthData(_data);
        }

        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputDragged += OnGetInputValues;
            InputSignals.Instance.onInputHandlerChange += OnDisableMovement;

            PlayerSignals.Instance.onHealthVisualOpen += OnHealthVisualOpen;
            PlayerSignals.Instance.onHealthVisualClose += OnHealthVisualClose;
            PlayerSignals.Instance.onHealthUpgrade += OnHealthUpdate;

            CoreGameSignals.Instance.onTakePlayerDamage += OnTakeDamage;
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputDragged -= OnGetInputValues;
            InputSignals.Instance.onInputHandlerChange -= OnDisableMovement;

            PlayerSignals.Instance.onHealthVisualClose -= OnHealthVisualClose;
            PlayerSignals.Instance.onHealthVisualOpen -= OnHealthVisualOpen;
            PlayerSignals.Instance.onHealthUpgrade -= OnHealthUpdate;

            CoreGameSignals.Instance.onTakePlayerDamage -= OnTakeDamage;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion

        private void OnGetInputValues(HorizontalInputParams inputParams)
        {
            movementController.UpdateInputValues(inputParams);
            animationController.PlayAnimation(inputParams);
        }
        public void SetEnemyTarget()
        {
            shootingController.SetEnemyTargetTransform();
            animationController.AimTarget(true);
        }
        private void OnHealthVisualClose() => playerHealthController.IsActive(false);
        private void OnHealthVisualOpen() => playerHealthController.IsActive(true);
        private void OnHealthUpdate() => playerHealthController.IncreaseHealth();
        private void OnTakeDamage(int value) => playerHealthController.TakeDamage(value);
        public void CheckAreaStatus(AreaTypes areaType) => meshController.ChangeAreaStatus(CurrentAreaType = areaType);
        private void OnDisableMovement(InputType inputType) => movementController.DisableMovement(inputType);
        public void SetTurretAnim(bool onTurret) => animationController.PlayTurretAnimation(onTurret);

        public void ResetPlayer()
        {
            LevelSignals.Instance.onLevelFailed?.Invoke();
            playerWalletController.Col.enabled = false;
            playerMoneyStackerController.ResetStack();
            PlayerSignals.Instance.onResetPlayerStack?.Invoke();
            DOVirtual.DelayedCall(.3f, () => animationController.DeathAnimation());
            playerPhysicsController.ResetPlayerLayer();
            EnemyTarget = null;
            EnemyList.Clear();
            CheckAreaStatus(AreaTypes.BaseDefense);
            CoreGameSignals.Instance.onReset?.Invoke();
            OnDisableMovement(InputType.None);
            DOVirtual.DelayedCall(3f, () =>
            {
                playerWalletController.Col.enabled = true;
                PlayerSignals.Instance.onHealthVisualOpen?.Invoke();
                playerHealthController.IncreaseHealth();
                transform.position = Vector3.zero;
                CoreGameSignals.Instance.onPlay?.Invoke();
                animationController.ChangeAnimations(PlayerAnimationStates.Idle);

            });
        }
        private void OnPreNextLevel()
        {
            animationController.ChangeAnimations(PlayerAnimationStates.Idle);
            OnDisableMovement(InputType.None);
            CoreGameSignals.Instance.onReset?.Invoke();
            playerWalletController.Col.enabled = false;
            PlayerSignals.Instance.onHealthUpgrade?.Invoke();
            PlayerSignals.Instance.onHealthVisualClose?.Invoke();
            EnemyTarget = null;
            EnemyList.Clear();
            playerMoneyStackerController.ResetStack();
            playerPhysicsController.ResetPlayerLayer();
            PlayerSignals.Instance.onResetPlayerStack?.Invoke();
            CheckAreaStatus(AreaTypes.BaseDefense);
            animationController.gameObject.SetActive(false);
        }
        private void OnNextLevel()
        {
            animationController.gameObject.SetActive(true);
            PlayerSignals.Instance.onHealthUpgrade?.Invoke();
            PlayerSignals.Instance.onHealthVisualClose?.Invoke();
            playerPhysicsController.ResetPlayerLayer();
            EnemyTarget = null;
            EnemyList.Clear();
            CheckAreaStatus(AreaTypes.BaseDefense);
            transform.position = Vector3.zero;
            animationController.ChangeAnimations(PlayerAnimationStates.Idle);
            playerWalletController.Col.enabled = true;
            CoreGameSignals.Instance.onReset?.Invoke();
            CoreGameSignals.Instance.onPlay?.Invoke();
        }
    }
}