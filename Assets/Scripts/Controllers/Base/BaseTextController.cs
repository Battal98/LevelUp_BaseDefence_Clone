using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Signals;

namespace Controllers
{
    public class BaseTextController : MonoBehaviour
    {
        #region Self Variables

        #region Serializable Variables

        [SerializeField]
        private TextMeshPro baseText;

        #endregion

        #endregion

        public void SetBaseLevelText()
        {
            baseText.text = "BASE " + (LevelSignals.Instance.onGetLevel() + 1);
        }
    }
}
