using System;
using Data.ValueObject;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class SaveLoadSignals : MonoSingleton<SaveLoadSignals>
    {
        //TODO:Save Atacagın datalara gore sinyal ac
     
        public UnityAction<ExampleSaveData,int> onSaveExampleData=delegate {  };
        public Func<string,int,ExampleSaveData> onLoadExampleData= delegate { return default;};
    }
}