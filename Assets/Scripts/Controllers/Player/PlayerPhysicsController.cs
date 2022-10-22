using Interfaces;
using Enums;
using Managers;
using UnityEngine;
using Signals;

namespace Controllers
{
    public class PlayerPhysicsController : Interactable
    {
        #region Self Variables

        #region Public Variables
        public bool IsTaken { get ; set ; }
        public bool IsDead { get; set; }
        
        #endregion

        #region Serialized Variables,

        [SerializeField] private PlayerManager playerManager;

        #endregion

        #region Private Variables

        #endregion

        #endregion

        public Transform GetTransform()
        {
            return playerManager.transform;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out GatePhysicsController physicsController))
            {
                var playerIsGoingToFrontYard = other.transform.position.z > transform.position.z;
                gameObject.layer =  LayerMask.NameToLayer("Base");
                playerManager.CheckAreaStatus(playerIsGoingToFrontYard ? AreaTypes.BattleOn : AreaTypes.BaseDefense);
                playerManager.EnemyTarget = null;
                playerManager.EnemyList.Clear();
            }

            if (other.TryGetComponent(out TurretPhysicController turretPhysicsController))
            {
                playerManager.SetTurretAnim(true);
            }

            if (other.TryGetComponent(out IDamager damager))
            {
                CoreGameSignals.Instance.onTakePlayerDamage(damager.Damage());
            }

            if (other.CompareTag("Finish"))
            {
                CoreGameSignals.Instance.onPreNextLevel?.Invoke();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out GatePhysicsController physicsController))
            {
                var playerIsGoingToFrontYard = other.transform.position.z < transform.position.z;
                gameObject.layer = LayerMask.NameToLayer(playerIsGoingToFrontYard? "BattleYard" : "Base");
                playerManager.CheckAreaStatus(playerIsGoingToFrontYard ? AreaTypes.BattleOn : AreaTypes.BaseDefense);
                if (!playerIsGoingToFrontYard)
                {
                    PlayerSignals.Instance.onHealthUpgrade?.Invoke();
                    int enemyListCount = playerManager.EnemyList.Count;
                    for (int i = 0; i < enemyListCount; i++)
                    {
                        playerManager.EnemyList[i].IsTaken = false;
                    }
                    playerManager.EnemyTarget = null;
                    playerManager.EnemyList.Clear();
                    return;
                }
                PlayerSignals.Instance.onHealthVisualOpen?.Invoke();

            }

            if (other.TryGetComponent(out TurretPhysicController turretPhysicsController))
            {
                playerManager.SetTurretAnim(false);
            }
        }

        public void ResetPlayerLayer()
        {
            gameObject.layer = LayerMask.NameToLayer("Base");
        }
    }
}