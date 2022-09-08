using UnityEngine;
using Controllers;

namespace Managers
{
    public class BaseManager : MonoBehaviour
    {
        #region Self Variables

        #region Serializable Variables

        [SerializeField]
        private BaseTextController baseTextController;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            baseTextController.SetBaseLevelText();
        }
    } 
}
