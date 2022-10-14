using UnityEngine;
using Managers;
using Interfaces;
using Enums;

namespace Controllers
{
    public class PlayerDetectionController : MonoBehaviour
    {
        [SerializeField]
        private PlayerManager manager;
        private void OnTriggerEnter(Collider other)
        {
            if (manager.CurrentAreaType == AreaTypes.BaseDefense) return;
            if (other.TryGetComponent(out IDamageable damagable))
            {
                if (damagable.IsTaken) return;
                manager.EnemyList.Add(damagable);
                damagable.IsTaken = true;
                if (manager.EnemyTarget == null)
                {
                    manager.SetEnemyTarget();
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damagable))
            {
                damagable.IsTaken = false;
                manager.EnemyList.Remove(damagable);
                manager.EnemyList.TrimExcess();
                if (manager.EnemyList.Count == 0)
                {
                    manager.EnemyTarget = null;
                    manager.HasEnemyTarget = false;
                }
            }
        }
    }
}