using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Controllers
{
    public class PortalController : MonoBehaviour
    {
        #region Serializable Variables

        [SerializeField]
        private List<MeshRenderer> portalMeshRenderers = new List<MeshRenderer>();

        [SerializeField]
        private Collider portalCollider;

        [SerializeField]
        private float dissolveOpenValue = 6;
        [SerializeField]
        private float dissolveCloseValue = 35; 

        #endregion

        #region Private Variables

        private const float dissolveTime = 2f;
        private const string dissolveName = "_DissolveAmount";

        #endregion

        private void Awake()
        {
            portalCollider.enabled = false;
        }
        public void OpenPortal()
        {
            for (int i = 0; i < portalMeshRenderers.Count; i++)
            {
                portalMeshRenderers[i].material.DOFloat(dissolveOpenValue, dissolveName, dissolveTime);
            }
            DOVirtual.DelayedCall(dissolveTime, () => portalCollider.enabled = true);
        }

        public void ClosePortal()
        {
            for (int i = 0; i < portalMeshRenderers.Count; i++)
            {
                portalMeshRenderers[i].material.SetFloat(dissolveName, dissolveCloseValue);
            }
            portalCollider.enabled = false;
        }

    } 
}
