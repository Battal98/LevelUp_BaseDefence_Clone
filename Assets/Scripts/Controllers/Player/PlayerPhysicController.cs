using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysicController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GateExit"))
        {
            gameObject.layer = LayerMask.NameToLayer("BattleYard");
        }

        if (other.CompareTag("GateEntry"))
        {
            gameObject.layer = LayerMask.NameToLayer("Base");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

}
