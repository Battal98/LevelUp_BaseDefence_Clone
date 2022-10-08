using UnityEngine;
using Interfaces;

namespace Extentions
{
    public class StackableMoney : AStackable
    {
        [SerializeField] private Rigidbody rbody;
        [SerializeField] private BoxCollider col;

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

        public override void SendPosition(Transform transform)
        {
            base.SendPosition(transform);
        }

        private void OnEnable()
        {
            SendPosition(this.transform);
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