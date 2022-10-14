using Enums;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class UISignals : MonoSingleton<UISignals>
    {
        public UnityAction<UIPanels> onOpenPanel;
        public UnityAction<UIPanels> onClosePanel;
        public UnityAction<int> onUpdateMoneyScore = delegate (int arg0) { };
        public UnityAction<int> onUpdateGemScore = delegate (int arg0) { };
    }
}