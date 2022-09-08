using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extentions;
using UnityEngine.Events;
using Enums;

public class GateSignals : MonoSingleton<GateSignals>
{
    public UnityAction<GateType> onChangeGateState = delegate { };
}
