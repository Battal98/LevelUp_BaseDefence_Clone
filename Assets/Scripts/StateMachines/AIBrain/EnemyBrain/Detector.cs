using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class Detector : MonoBehaviour
{
    private PlayerManager _detectedPlayer;
    private Transform _detectedMine;
    public bool IsPlayerInRange() => _detectedPlayer != null;
    public bool IsBombInRange() => _detectedMine != null; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>())
        {
            _detectedPlayer = other.GetComponent<PlayerManager>();
        }

        /*if (other.GetComponent<Mine>())
        {
            _detectedMine = other.GetComponent<Mine>();
        }*/
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerManager>())
        {
            StartCoroutine(ClearDetectedAfterDelay(_detectedPlayer.gameObject));
        }

        /*if (other.GetComponent<Mine>())
        {

        }*/
    }

    private IEnumerator ClearDetectedAfterDelay(GameObject gO)
    {
        yield return new WaitForSeconds(0.1f);
        gO = null;
    }

    public Vector3 GetNearestPosition(GameObject gO)
    {
        return gO?.transform.position ?? Vector3.zero;
    }
}
