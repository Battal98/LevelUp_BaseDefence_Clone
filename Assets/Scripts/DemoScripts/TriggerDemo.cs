using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Managers;

public class TriggerDemo : MonoBehaviour
{
    public Transform target;
    public List<GameObject> AllObjects = new List<GameObject>();
    public List<GameObject> ActiveObjects = new List<GameObject>();
    public NavMeshAgent agent;
    public int targetIndex;
    public Transform SpawmPos;

    private void Awake()
    {
        //pool sinyal ile collectables money listesini yolla 
        target = SpawmPos;
        agent.SetDestination(target.position);  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectables"))
        {
            if (!ActiveObjects.Contains(other.gameObject))
            {
                ActiveObjects.Add(other.gameObject);
            }
            
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Collectables"))
        {
            if (target != null && target.TryGetComponent(out CollectableManager collectables)) 
            {
                Debug.Log( "Dist: "+ Vector3.Distance(this.transform.position, target.transform.position));
                if (Vector3.Distance(this.transform.position, target.transform.position) <= 0.1f)
                {
                    FindNewTarget(ActiveObjects[targetIndex].gameObject);
                }

            }

        }
    }

    private void FindNewTarget(GameObject removableObj)
    {
        Debug.Log("findnewTarget");
        if (ActiveObjects.Count>0)
        {
            removableObj.SetActive(false);
            ActiveObjects.Remove(removableObj);
            ActiveObjects.TrimExcess();
            targetIndex = Random.Range(0, ActiveObjects.Count);
            if (ActiveObjects[targetIndex].activeInHierarchy)
            {
                target = ActiveObjects[targetIndex].transform;
                agent.SetDestination(target.position);
            }
            else
            {
                target = SpawmPos;
                agent.SetDestination(target.position);
            }

        }


    }
}
