using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{

    public class ParticleEmitController : MonoBehaviour
    {
        #region Self Variables

        #region Serializable Variables

        [SerializeField] private Vector3 emitPositionAdjust;
        [SerializeField] private float particleStartSize;
        [SerializeField] private int burstCount;

        #endregion

        #region Private Variables

        private ParticleSystem _particleSystem;
        private Renderer _particleSystemRenderer;

        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            _particleSystemRenderer = GetComponent<Renderer>();
            _particleSystem.Stop();
            transform.Rotate(0, 0, 0);
        }

        public void EmitParticle(Vector3 _position, Quaternion toRotation)
        {
            gameObject.SetActive(true);
            var emitParams = new ParticleSystem.EmitParams()
            {
                position = _position + emitPositionAdjust,
                startSize = particleStartSize,
                
            };
            _particleSystem.transform.rotation = toRotation;
            _particleSystem.Emit(emitParams, burstCount);
        }

        public void EmitParticleWithSetColor(Vector3 _position, Quaternion toRotation, Color _color)
        {
            gameObject.SetActive(true);
            var emitParams = new ParticleSystem.EmitParams()
            {
                position = _position + emitPositionAdjust,
                startColor = _color,
                startSize = particleStartSize,

            };
            _particleSystemRenderer.material.color = _color;
            _particleSystem.transform.rotation = toRotation;
            _particleSystem.Emit(emitParams, burstCount);
        }

        public void StopEmit()
        {
            _particleSystem.Stop();
            gameObject.SetActive(false);
        }
    } 
}
