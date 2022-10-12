using Interfaces;
using Enums;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class FireController : IGetPoolObject
    {
        private WeaponType _weaponTypes;
        public FireController(WeaponType weaponType)
        {
            _weaponTypes = weaponType;
        }
        public GameObject GetObjectType(PoolType poolName)
        {
            var obj = PoolSignals.Instance.onGetObjectFromPool.Invoke(poolName);
            return obj;
        }
        public void FireBullets(Transform aim)
        {
            var poolType = (PoolType)System.Enum.Parse(typeof(PoolType), _weaponTypes.ToString());
            var bullet = GetObjectType(poolType);
            Debug.Log("fireBullet: " + bullet + " Aim: " + aim);
            bullet.transform.position = aim.position;
            bullet.transform.rotation = aim.rotation;
        }
    }
}