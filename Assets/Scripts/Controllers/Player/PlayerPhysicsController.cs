using Interfaces;
using Controllers;
using Enums;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class PlayerPhysicsController : Interactable
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables,
        
        [SerializeField] private PlayerManager playerManager;
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
                playerManager.SetTurretAnimation(true);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out GatePhysicsController physicsController))
            {
                var playerIsGoingToFrontYard = other.transform.position.z < transform.position.z;
                gameObject.layer = LayerMask.NameToLayer(playerIsGoingToFrontYard? "BattleYard" : "Base");
                playerManager.CheckAreaStatus(playerIsGoingToFrontYard ? AreaTypes.BattleOn : AreaTypes.BaseDefense);
            }

            if (other.TryGetComponent(out TurretPhysicController turretPhysicsController))
            {
                playerManager.SetTurretAnimation(false);
            }
        }
    }
}