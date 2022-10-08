using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Extentions;
using Enums;
using Controllers;
public class ParticleSignals : MonoSingleton<ParticleSignals>
{
    public UnityAction<ParticleType, Vector3, Quaternion> onPlayParticle = delegate {  };
    public UnityAction<ParticleType, Vector3, Quaternion, Color> onPlayParticleWithSetColor = delegate {  };
    public UnityAction<ParticleType> onStopParticle = delegate {  };
    public UnityAction<List<ParticleEmitController>> onStopAllParticle = delegate {  };
}
