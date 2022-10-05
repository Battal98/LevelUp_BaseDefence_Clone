using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Commands;
public class SaveLoadManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    #endregion

    #region Seriliazable Variables

    #endregion  
    
    #region Private Variables

    private SaveGameCommand _saveGameCommand = new SaveGameCommand();
    private LoadGameCommand _loadGameCommand = new LoadGameCommand();

    #endregion

    #endregion

    private void Awake()
    {
        Initilization();
    }
    private void Initilization()
    {
        _saveGameCommand = new SaveGameCommand();
        _loadGameCommand = new LoadGameCommand();
    }

    #region Subscription Events

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {

    }

    private void UnsubscribeEvents()
    {

    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }


    #endregion

}
