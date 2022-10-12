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

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public AreaTypes CurrentAreaType = AreaTypes.BaseDefense;
        public WeaponType WeaponType;

        public List<IDamagable> EnemyList = new List<IDamagable>();

        public Transform EnemyTarget;

        public bool HasEnemyTarget = false;

        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerMeshController meshController;
        [SerializeField] private PlayerAnimationController animationController;
        [SerializeField] private PlayerWeaponController weaponController;
        [SerializeField] private PlayerShootingController playerShootingController;

        #endregion

        #region Private Variables
        
        private PlayerData _data;

        private WeaponData _weaponData;

        private PlayerMovementController _movementController;
        
        #endregion
        
        #endregion
        private void Awake()
        {
            _data = GetPlayerData();
            _weaponData = GetWeaponData();
            Init();
            CoreGameSignals.Instance.onSetCameraTarget?.Invoke(this.transform);
        }
        private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_Player").PlayerData;
        private WeaponData GetWeaponData() => Resources.Load<CD_Weapons>("Data/CD_Weapons").WeaponDatas[(int)WeaponType];
        private void Init()
        {
            _movementController = GetComponent<PlayerMovementController>();
            SetDataToControllers();
        }
        private void SetDataToControllers()
        {
            _movementController.SetMovementData(_data.PlayerMovementData);
            weaponController.SetWeaponData(_weaponData);
            meshController.SetWeaponData(_weaponData);
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
        }
        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputDragged -= OnGetInputValues;
            InputSignals.Instance.onInputHandlerChange -= OnDisableMovement;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        private void OnGetInputValues(HorizontalInputParams inputParams)
        {
            _movementController.UpdateInputValues(inputParams);
            animationController.PlayAnimation(inputParams);
            if (!HasEnemyTarget) return;
            AimEnemy();
        }
        public void CheckAreaStatus(AreaTypes AreaStatus)
        {
            CurrentAreaType = AreaStatus;
            meshController.ChangeAreaStatus(AreaStatus);
        }
        private void OnDisableMovement(InputType ınputHandlers)
        {
            if (ınputHandlers == InputType.Turret)
            {
                _movementController.DisableMovement();
            }
        }

        public void SetTurretAnimation(bool isTurretHolded)
        {
            animationController.HoldTurret(isTurretHolded);
        }

        public void SetEnemyTarget()
        {
            playerShootingController.SetEnemyTargetTransform();
            animationController.AimTarget(true);
            AimEnemy();
        }
        private void AimEnemy()
        {
            if (EnemyList.Count != 0)
            {
                var transformEnemy = EnemyList[0].GetTransform();
                _movementController.RotatePlayerToTarget(transformEnemy);
            }
        }
    }
}