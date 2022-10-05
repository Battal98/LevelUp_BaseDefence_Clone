using System;
using Enums;
using Extentions;
using UnityEngine;

namespace Signals
{
    public class MineBaseSignals:MonoSingleton<MineBaseSignals>
    {
        public Func<Tuple<Transform,GemMineType>> onGetRandomMineTarget= delegate { return null;};
        public Func<Transform> onGetGemHolderPos= delegate { return null;};
    }
}