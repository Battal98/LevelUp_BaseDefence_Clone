using Data.ValueObject.WeaponData;
using Enums;
using Keys;
using UnityEngine;

namespace Controllers
{
    public class PlayerMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables,
         
        [SerializeField] private MeshRenderer weaponMeshRenderer;
        [SerializeField] private MeshRenderer sideMeshRenderer;

        #endregion

        #region Private Variables

        private WeaponData _data;
        
        #endregion

        #endregion
        public void SetWeaponData(WeaponData weaponData)
        {
            _data = weaponData;
        }
        
        public void ChangeAreaStatus(AreaTypes areaStatus)
        {
            if (areaStatus == AreaTypes.BaseDefense)
            {
                weaponMeshRenderer.enabled = false;
                sideMeshRenderer.enabled = false;
            }
            else
            {
                weaponMeshRenderer.enabled = true;
                sideMeshRenderer.enabled = _data.HasSideMesh;
            }
        }
    }
}