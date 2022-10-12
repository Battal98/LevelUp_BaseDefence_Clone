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
        public void FireBullets(Transform holderTransform)
        {
            var poolType = (PoolType)System.Enum.Parse(typeof(PoolType), _weaponTypes.ToString());
            var bullet = GetObjectType(poolType);
            bullet.transform.position = holderTransform.position;
            bullet.transform.rotation = holderTransform.rotation;
        }
    }
}