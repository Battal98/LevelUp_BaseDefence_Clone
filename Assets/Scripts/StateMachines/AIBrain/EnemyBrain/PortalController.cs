using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Controllers
{
    public class PortalController : MonoBehaviour
    {
        [SerializeField]
        private List<MeshRenderer> portalMeshRenderers = new List<MeshRenderer>();

        [SerializeField]
        private Collider portalCollider;

        [SerializeField]
        private float dissolveValue = 6;

        private void Awake()
        {
            portalCollider.enabled = false;
        }
        public void OpenPortal()
        {
            for (int i = 0; i < portalMeshRenderers.Count; i++)
            {
                portalMeshRenderers[i].material.DOFloat(dissolveValue, "_DissolveAmount", 2f);
            }
            portalCollider.enabled = true;
        }
    } 
}
