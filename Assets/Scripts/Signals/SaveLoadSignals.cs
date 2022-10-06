using System;
using Data.ValueObject;
using Data.UnityObject;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class SaveLoadSignals : MonoSingleton<SaveLoadSignals>
    {
        //TODO:Save Atacagın datalara gore sinyal ac
     
        public UnityAction<CD_Level,int> onSaveGameData = delegate {  };
        public Func<string,int, CD_Level> onLoadGameData;
    }
}