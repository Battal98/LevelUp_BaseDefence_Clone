
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
namespace Controllers
{
    public class PlayerPhysicController : MonoBehaviour
    {
        [SerializeField]
        private PlayerLayerType playerLayerType;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Gate"))
            {

                if (other.transform.position.z > transform.position.z)
                {
                    playerLayerType = PlayerLayerType.Base;
                    gameObject.layer = LayerMask.NameToLayer(playerLayerType.ToString());
                }
                else
                {
                    playerLayerType = PlayerLayerType.Base;
                    gameObject.layer = LayerMask.NameToLayer(playerLayerType.ToString());
                }

            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Gate"))
            {

                if (other.transform.position.z < transform.position.z)
                {
                    playerLayerType = PlayerLayerType.BattleYard;
                    gameObject.layer = LayerMask.NameToLayer(playerLayerType.ToString());
                }
                else
                {
                    playerLayerType = PlayerLayerType.Base;
                    gameObject.layer = LayerMask.NameToLayer(playerLayerType.ToString());

                }

            }

        }

    } 
}
