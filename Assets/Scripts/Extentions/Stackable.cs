using UnityEngine;
using Interfaces;

namespace Extentions
{
    public class Stackable : AStackable
    {
        [SerializeField] private Rigidbody rbody;
        [SerializeField] private BoxCollider col;

        public override bool IsSelected { get; set; }
        public override bool IsCollected { get; set; }

        public override void SetInit(Transform initTransform, Vector3 position)
        {
            base.SetInit(initTransform, position);
        }

        public override void SetVibration(bool isVibrate)
        {
            base.SetVibration(isVibrate);
        }

        public override void SetSound()
        {
            base.SetSound();
        }

        public override void EmitParticle()
        {
            base.EmitParticle();
        }

        public override void PlayAnimation()
        {
            base.PlayAnimation();
        }

        public override void SendStackable(Stackable transform)
        {
            base.SendStackable(transform);
        }

        private void OnEnable()
        {
            rbody.useGravity = true;
            rbody.isKinematic = false;
            col.enabled = true;
            SendStackable(this);
        }

        public override GameObject SendToStack()
        {
            rbody.useGravity = false;
            rbody.isKinematic = true;
            col.enabled = false;
            return transform.gameObject;
        }
    }
}