using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Controllers;
using Data.ValueObject.WeaponData;
using Data.UnityObject;
using Interfaces;
using Signals;

namespace Managers
{
    public class BulletManager : MonoBehaviour, IReleasePoolObject
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField]
        private WeaponType weaponType;
        [SerializeField]
        private BulletPhysicControllerG physicsController;

        #endregion

        #region Private Variables

        private WeaponData _data;

        #endregion

        #endregion
        private void Awake()
        {
            _data = GetBulletData();
            SetDataToControllers();
        }
        private void OnEnable()
        {
            Invoke(nameof(SetBulletToPool), 1f);
        }
        private WeaponData GetBulletData() => Resources.Load<CD_Weapons>("Data/CD_Weapons").WeaponDatas[(int)weaponType];
        private void SetDataToControllers() => physicsController.GetData(_data);
        public void ReleaseObject(GameObject obj, PoolType poolName) => PoolSignals.Instance.onReleaseObjectFromPool.Invoke(poolName, obj);
        public void SetBulletToPool()
        {
            var poolName = (PoolType)System.Enum.Parse(typeof(PoolType), weaponType.ToString());
            ReleaseObject(gameObject, poolName);
        }
    } 
}
