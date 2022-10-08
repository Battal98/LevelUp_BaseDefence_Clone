using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Controllers;

namespace Interfaces
{
    public abstract class AParticleSystem : MonoBehaviour, IPartlicleSystem
    {
        public abstract ParticleEmitController ParticleEmitController { get; set; }
        public virtual void OnPlayParticle(ParticleType particleType, Vector3 position, Quaternion toRotation)
            => ParticleEmitController.EmitParticle(position,toRotation);
        public virtual void OnPlayParticleWithSetColor(ParticleType particleType, Vector3 position, Quaternion toRotation, Color color)
            =>  ParticleEmitController.EmitParticleWithSetColor(position, toRotation, color);

        public virtual void OnStopAllParticle(List<ParticleEmitController> particleEmits)
        {
            for (int i = 0; i < particleEmits.Count; i++)
            {
                particleEmits[i].StopEmit();
            }
        }

        public virtual void OnStopParticle(ParticleType particleType) => ParticleEmitController.StopEmit();

    } 
}
