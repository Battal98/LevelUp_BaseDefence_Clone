/// <summary>
/// Author Battal Yigit PATLAR - in LevelUp Academy
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using Enums;
using Controllers;
using Signals;
using Sirenix.OdinInspector;

namespace Managers
{

    public class ParticleManager : AParticleSystem
    {
        #region Self Variables

        #region Serializable Variables

        [SerializeField] 
        private List<ParticleEmitController> particleEmitControllers = new List<ParticleEmitController>();

        public override ParticleEmitController ParticleEmitController { get; set; }

        #endregion

        #region Private Variables



        #endregion

        #endregion

        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            ParticleSignals.Instance.onPlayParticle += OnPlayParticle;
            ParticleSignals.Instance.onPlayParticleWithSetColor += OnPlayParticleWithSetColor;
            ParticleSignals.Instance.onStopParticle += OnStopParticle;
            ParticleSignals.Instance.onStopAllParticle += OnStopAllParticle;
        }
        private void UnsubscribeEvents()
        {
            ParticleSignals.Instance.onPlayParticle -= OnPlayParticle;
            ParticleSignals.Instance.onPlayParticleWithSetColor -= OnPlayParticleWithSetColor;
            ParticleSignals.Instance.onStopParticle -= OnStopParticle;
            ParticleSignals.Instance.onStopAllParticle -= OnStopAllParticle;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        /// <summary>
        /// The function that listens where you want to play the particle.
        /// </summary>
        /// <param name="particleType"></param>
        /// <param name="position"></param>
        /// <param name="toRotation"></param>
        public override void OnPlayParticle(ParticleType particleType, Vector3 position, Quaternion toRotation)
        {
            ParticleEmitController = particleEmitControllers[(int)particleType];
            base.OnPlayParticle(particleType, position,toRotation);
        }

        /// <summary>
        /// The function that listens where you want to play the particle and change particle color.
        /// </summary>
        /// <param name="particleType"></param>
        /// <param name="position"></param>
        /// <param name="toRotation"></param>
        /// <param name="color"></param>
        public override void OnPlayParticleWithSetColor(ParticleType particleType, Vector3 position, Quaternion toRotation, Color color)
        {
            ParticleEmitController = particleEmitControllers[(int)particleType];
            base.OnPlayParticleWithSetColor(particleType, position, toRotation, color);
        }

        /// <summary>
        /// Stop All particle Emits
        /// </summary>
        /// <param name="particleEmits"> Particle Emit Controller lists</param>

        public override void OnStopAllParticle(List<ParticleEmitController> particleEmits)
        {
            particleEmits = particleEmitControllers;
            base.OnStopAllParticle(particleEmits);
        }

        /// <summary>
        /// Stop Spesific particle emit
        /// </summary>
        /// <param name="particleType">type to be invoked in signal</param>
        public override void OnStopParticle( ParticleType particleType)
        {
            ParticleEmitController = particleEmitControllers[(int)particleType]; ;
            base.OnStopParticle(particleType);
        }


        /// <summary>
        /// if using Odin Inspector, Open this line and Tested invokes
        /// </summary>
        /// <param name="type">Particle Type</param>

        /*
        [SerializeField]
        private Transform target;

        [Button("Play Particle")]

        public void ParticlePlay(ParticleType type)
        {
            ParticleSignals.Instance.onPlayParticle?.Invoke(type, target.position,target.rotation);
        }

        [Button("Stop Particle")]

        public void StopParticle(ParticleType type)
        {
            ParticleSignals.Instance.onStopParticle?.Invoke(type);
        }
        */
    }
}
