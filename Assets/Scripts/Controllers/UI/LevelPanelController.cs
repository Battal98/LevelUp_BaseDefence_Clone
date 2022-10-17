using System;
using DG.Tweening;
using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class LevelPanelController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField]
        private TextMeshProUGUI gemText;
        [SerializeField]
        private TextMeshProUGUI moneyText;

        #endregion

        #endregion
        public void SetGemScoreText(int gemValue)
        {
            gemText.text = gemValue.ToString();
        }

        public void SetMoneyScoreText(int moneyValue)
        {
            moneyText.text = moneyValue.ToString();
        }
    }
}