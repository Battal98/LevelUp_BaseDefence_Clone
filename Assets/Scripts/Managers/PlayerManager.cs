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

        public List<IDamageable> EnemyList = new List<IDamageable>();

        public Transform EnemyTarget;

        public bool HasEnemyTarget = false;

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
            movementController.UpdateInputValues(inputParams);
            animationController.PlayAnimation(inputParams);
            AimEnemy();
        }
        public void SetEnemyTarget()
        {
            shootingController.SetEnemyTargetTransform();
            animationController.AimTarget(true);
            AimEnemy();
        }
        private void AimEnemy() => movementController.LookAtTarget(!HasEnemyTarget ? null : EnemyList[0]?.GetTransform());
        public void CheckAreaStatus(AreaTypes areaType) => meshController.ChangeAreaStatus(CurrentAreaType = areaType);
        private void OnDisableMovement(InputType inputType) => movementController.DisableMovement(inputType);
        public void SetTurretAnim(bool onTurret) => animationController.PlayTurretAnimation(onTurret);
    }
}