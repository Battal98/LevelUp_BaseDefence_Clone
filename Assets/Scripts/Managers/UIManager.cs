using Controllers;
using Enums;
using Signals;
using UnityEngine;
using TMPro;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private UIPanelController uiPanelController;
        [SerializeField] private LevelPanelController levelPanelController;

        #endregion

        #region Private Variables



        #endregion

        #endregion

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;

            CoreGameSignals.Instance.onGetGameState += OnGetGameState;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onUpdateMoneyScore += OnUpdateMoneyScore;
            CoreGameSignals.Instance.onUpdateGemScore += OnUpdateGemScore;

            LevelSignals.Instance.onLevelFailed += OnLevelFailed;
            LevelSignals.Instance.onLevelSuccessful += OnLevelSuccessful;

        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;

            CoreGameSignals.Instance.onGetGameState -= OnGetGameState;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onUpdateMoneyScore -= OnUpdateMoneyScore;
            CoreGameSignals.Instance.onUpdateGemScore -= OnUpdateGemScore;

            LevelSignals.Instance.onLevelFailed -= OnLevelFailed;
            LevelSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Awake()
        {
            InitPanels();
        }

        private void Start()
        {
            OnSetLevelText();
        }

        private void OnOpenPanel(UIPanels panelParam)
        {
            uiPanelController.OpenPanel(panelParam);
        }

        private void OnClosePanel(UIPanels panelParam)
        {
            uiPanelController.ClosePanel(panelParam);
        }

        private void InitPanels()
        {
            uiPanelController.OpenPanel(UIPanels.IdlePanel);
            /*uiPanelController.ClosePanel(UIPanels.StorePanel);
            uiPanelController.ClosePanel(UIPanels.TurretPanel);
            uiPanelController.ClosePanel(UIPanels.DronePanel);*/
        }

        private void OnSetLevelText()
        {
            levelPanelController.SetLevelText();
        }

        private void OnPlay()
        {
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.IdlePanel);
        }

        private void OnLevelFailed()
        {
            //UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
        }

        private void OnLevelSuccessful()
        {
            LevelSignals.Instance.onLevelSuccessful?.Invoke(); // Trigger in Final 
        }

        public void Play()
        {
            CoreGameSignals.Instance.onPlay?.Invoke();
        }

        public void NextLevel()
        {
            LevelSignals.Instance.onNextLevel?.Invoke();
            OnSetLevelText();
        }

        public void Restart()
        {
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.IdlePanel);
            LevelSignals.Instance.onRestartLevel?.Invoke();
        }


        private void OnGetGameState(GameStates states)
        {
            switch (states)
            {
                case GameStates.Idle:
                    UISignals.Instance.onClosePanel?.Invoke(UIPanels.IdlePanel);
                    break;
            }
        }

        private int totalGemValue;
        private int totalMoneyValue;
        [SerializeField]
        private TextMeshProUGUI gemText;
        [SerializeField]
        private TextMeshProUGUI moneyText;

        private void OnUpdateGemScore(int gemValue)
        {
            totalGemValue += gemValue;
            gemText.text = totalGemValue.ToString();

        }

        private void OnUpdateMoneyScore(int moneyValue)
        {
            totalMoneyValue += moneyValue;
            moneyText.text = totalMoneyValue.ToString();
        }
    }
}