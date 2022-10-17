using Interfaces;
using Controllers;
using Enums;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class PlayerPhysicsController : Interactable, IDamageable
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables,
        
        [SerializeField] private PlayerManager playerManager;

        public bool IsTaken { get ; set ; }
        public bool IsDead { get; set; }

        public Transform GetTransform()
        {
            return playerManager.transform;
        }

        public int TakeDamage(int damage)
        {
            if (playerManager.Health > 0)
            {
                playerManager.Health -= damage;
                if (playerManager.Health <= 0)
                {
                    IsDead = true;
                    return playerManager.Health;
                }
                return playerManager.Health;
            }
            return 0;
        }
        #endregion

        #region Private Variables

        #endregion

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out GatePhysicsController physicsController))
            {
                var playerIsGoingToFrontYard = other.transform.position.z > transform.position.z;
                gameObject.layer =  LayerMask.NameToLayer("Base");
                playerManager.CheckAreaStatus(playerIsGoingToFrontYard ? AreaTypes.BattleOn : AreaTypes.BaseDefense);
            }

            if (other.TryGetComponent(out TurretPhysicController turretPhysicsController))
            {
                playerManager.SetTurretAnim(true);
            }

            if (other.TryGetComponent(out IDamager damager))
            {
                Debug.Log("Bomb");
                TakeDamage(damager.Damage());
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out GatePhysicsController physicsController))
            {
                var playerIsGoingToFrontYard = other.transform.position.z < transform.position.z;
                gameObject.layer = LayerMask.NameToLayer(playerIsGoingToFrontYard? "BattleYard" : "Base");
                playerManager.CheckAreaStatus(playerIsGoingToFrontYard ? AreaTypes.BattleOn : AreaTypes.BaseDefense);
                if (!playerIsGoingToFrontYard) return;
                playerManager.EnemyTarget = null;
                playerManager.EnemyList.Clear();
            }

            if (other.TryGetComponent(out TurretPhysicController turretPhysicsController))
            {
                playerManager.SetTurretAnim(false);
            }
        }
    }
}