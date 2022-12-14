using Commands;
using Data.UnityObject;
using Data.ValueObject;
using Signals;
using UnityEngine;

namespace Managers
{
    public class SaveLoadManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private LoadGameCommand _loadGameCommand;
        private SaveGameCommand _saveGameCommand;


        #endregion

        #endregion

        private void Awake()
        {
            Initialization();
        }

        private void Initialization()
        {
            _loadGameCommand = new LoadGameCommand();
            _saveGameCommand = new SaveGameCommand();
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            SaveLoadSignals.Instance.onSaveLevelData += _saveGameCommand.Execute;
            SaveLoadSignals.Instance.onLoadLevelData += _loadGameCommand.Execute<CD_Level>;
        }

        private void UnsubscribeEvents()
        {
            SaveLoadSignals.Instance.onSaveLevelData -= _saveGameCommand.Execute;
            SaveLoadSignals.Instance.onLoadLevelData -= _loadGameCommand.Execute<CD_Level>;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
    }
}