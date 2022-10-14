using System.Collections;
using UnityEngine;
using Managers;
using System.Threading.Tasks;


namespace Controllers
{
    public class PlayerShootingController : MonoBehaviour
    {

        #region Serializable Variables

        [SerializeField]
        private PlayerManager manager;
        [SerializeField]
        private Transform aim;

        #endregion

        #region Private Variables

        private FireController fireController; 

        #endregion
        private void Awake()
        {
            fireController = new FireController(manager.WeaponType);
        }
        public void SetEnemyTargetTransform()
        {
            manager.EnemyTarget = manager.EnemyList[0].GetTransform();
            manager.HasEnemyTarget = true;
            Shoot();
        }
        public void EnemyTargetStatus()
        {
            if (manager.EnemyList.Count != 0)
            {
                SetEnemyTargetTransform();
            }
            else
            {
                manager.HasEnemyTarget = false;
            }
        }
        public void RemoveTarget()
        {
            if (manager.EnemyList.Count == 0) return;
            manager.EnemyList.RemoveAt(0);
            manager.EnemyList.TrimExcess();
            manager.EnemyTarget = null;
            EnemyTargetStatus();
        }
        public async void Shoot()
        {
            if (!manager.EnemyTarget)
                return;
            if (manager.EnemyList[0].IsDead)
            {
                RemoveTarget();
            }
            else
            {
                await Task.Delay(400);
                fireController.FireBullets(aim);
                Shoot();
            }
        }
    }
}
