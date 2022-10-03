using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class SoldierAISignals : MonoSingleton<SoldierAISignals>
    {
        public UnityAction onSoldierActivation = delegate { };
    }
}
