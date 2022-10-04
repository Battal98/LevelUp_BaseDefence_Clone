using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Data.ValueObject.WeaponData;
using Enums;
using Keys;
using Signals;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [FormerlySerializedAs("CurrentGameState")] public AreaTypes currentAreaType = AreaTypes.BaseDefense;
        public WeaponType WeaponType;
        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerMeshController meshController;
        [SerializeField] private PlayerAnimationController animationController;
        [SerializeField] private PlayerWeaponController weaponController;

        #endregion

        #region Private Variables
        
        private PlayerData _data;

        private WeaponData _weaponData;

        private PlayerMovementController _movementController;

        private AreaTypes _nextState = AreaTypes.BattleOn;
        
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
        }
        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputDragged -= OnGetInputValues;
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
        }
        public void CheckAreaStatus(AreaTypes AreaStatus)
        {
            currentAreaType = AreaStatus;
            meshController.ChangeAreaStatus(AreaStatus);
        }
    }
}