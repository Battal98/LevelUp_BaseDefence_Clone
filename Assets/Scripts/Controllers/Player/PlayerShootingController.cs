using System.Collections;
using UnityEngine;
using Managers;
using Enums;


namespace Controllers
{
    public class PlayerShootingController : MonoBehaviour
    {
        [SerializeField]
        private PlayerManager manager;

        [SerializeField]
        private Transform weaponHolder;

        private FireController _fireController;
        private const float _fireRate = 0.3f;

        private void Awake()
        {
            _fireController = new FireController(manager.WeaponType);
        }
        public void SetEnemyTargetTransform()
        {
            manager.Damageable = manager.EnemyList[0];
            if (manager.Damageable.IsTaken)
            {
                RemoveTarget();
            }
            else
            {
                manager.Damageable.IsTaken = true;
                manager.EnemyTarget = manager.Damageable.GetTransform();
                Shoot();
            }
        }
        private void EnemyTargetStatus()
        {
            if (manager.EnemyList.Count != 0)
            {
                SetEnemyTargetTransform();
            }
            else
            {
                manager.EnemyTarget = null;
            }
        }
        private void RemoveTarget()
        {
            if (manager.EnemyList.Count == 0) return;
            manager.EnemyList.RemoveAt(0);
            manager.EnemyList.TrimExcess();
            manager.EnemyTarget = null;
            EnemyTargetStatus();
        }
        private void Shoot()
        {
            if (!manager.EnemyTarget || manager.CurrentAreaType == AreaTypes.BaseDefense)
                return;
            if (manager.EnemyList[0].IsDead)
            {
                RemoveTarget();
            }
            else
            {
                StartCoroutine(FireBullets());
            }
        }
        private IEnumerator FireBullets()
        {
            yield return new WaitForSeconds(_fireRate);
            _fireController.FireBullets(weaponHolder);
            Shoot();
        }
    }
}
