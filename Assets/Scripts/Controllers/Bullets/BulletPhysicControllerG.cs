using UnityEngine;
using Enums;
using Data.ValueObject.WeaponData;
using Interfaces;
using Managers;

namespace Controllers
{

    public class BulletPhysicControllerG : MonoBehaviour, IDamager
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField]
        private BulletManager bulletManager;

        #endregion

        #region Private Variables

        private int _damage;
        private readonly Vector3 _offset = Vector3.up;

        #endregion

        #endregion

        public void GetData(WeaponData data)
        {
            _damage = data.Damage;
        }
        public int Damage()
        {
            return _damage;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable idDamagable))
            {
                ParticleSignals.Instance.onPlayParticleWithSetColor(ParticleType.EnemyDeath, other.transform.position + _offset, Quaternion.identity, Color.red);
                bulletManager.SetBulletToPool();
            }
        }
    } 
}
