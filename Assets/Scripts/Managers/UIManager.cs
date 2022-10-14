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

        [SerializeField] 
        private UIPanelController uiPanelController;
        [SerializeField] 
        private LevelPanelController levelPanelController;
        [SerializeField]
        private TextMeshProUGUI gemText;
        [SerializeField]
        private TextMeshProUGUI moneyText;


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
            UISignals.Instance.onUpdateMoneyScore += OnUpdateMoneyScore;
            UISignals.Instance.onUpdateGemScore += OnUpdateGemScore;

            CoreGameSignals.Instance.onGetGameState += OnGetGameState;
            CoreGameSignals.Instance.onPlay += OnPlay;

            LevelSignals.Instance.onLevelFailed += OnLevelFailed;
            LevelSignals.Instance.onLevelSuccessful += OnLevelSuccessful;

        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            UISignals.Instance.onUpdateMoneyScore -= OnUpdateMoneyScore;
            UISignals.Instance.onUpdateGemScore -= OnUpdateGemScore;

            CoreGameSignals.Instance.onGetGameState -= OnGetGameState;
            CoreGameSignals.Instance.onPlay -= OnPlay;

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

        private void OnUpdateGemScore(int gemValue)
        {

            gemText.text = gemValue.ToString();
        }

        private void OnUpdateMoneyScore(int moneyValue)
        {
            moneyText.text = moneyValue.ToString();
        }
    }
}