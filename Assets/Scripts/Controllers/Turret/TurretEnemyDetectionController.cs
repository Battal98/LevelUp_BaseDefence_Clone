using System;
using Interfaces;
using UnityEngine;

namespace Controllers
{
    public class TurretEnemyDetectionController : MonoBehaviour
    {
        [SerializeField] private TurretShootController shootController;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out IDamagable damageable)) return;
            print("ENEMY!");
            if (!damageable.IsTaken)
                shootController.EnemyInRange(damageable.GetTransform().gameObject);
            shootController.ShootTheTarget();

        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out IDamagable damageable)) return;
            print("ENEMY!");
            shootController.EnemyOutOfRange(damageable.GetTransform().gameObject);
            damageable.IsTaken = false;

        }
    }
}