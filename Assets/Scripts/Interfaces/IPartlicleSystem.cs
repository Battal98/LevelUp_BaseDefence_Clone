using UnityEngine;
using Enums;
using System.Collections.Generic;
using Controllers;

public interface IPartlicleSystem 
{
    void OnPlayParticle(ParticleType particleType,Vector3 position,Quaternion toRotation);
    void OnStopParticle(ParticleType particleType);
    void OnStopAllParticle(List<ParticleEmitController> particleEmits);
    void OnPlayParticleWithSetColor(ParticleType particleType, Vector3 position, Quaternion toRotation, Color color);

}
