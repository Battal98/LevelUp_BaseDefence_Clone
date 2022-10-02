using System;
using System.Collections.Generic;
using Interfaces;
using Concrete;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;
using Task = System.Threading.Tasks.Task;


namespace Controllers
{
    [RequireComponent(typeof(StackController))]
    public class MoneyStackerController : AStacker
    {
        [SerializeField] private List<Vector3> positionList;

        [SerializeField] private float radiusAround;

        //private float stackDelay = 0.5f;

        private Sequence GetStackSequence;

        //private int stackListOrder = 0;

        private int stackListConstCount;

        private bool canRemove = true;


        private void Awake()
        {
            DOTween.Init(true, true, LogBehaviour.Verbose).SetCapacity(500, 125);
        }

        private List<GameObject> StackLists
        {
            get => base.StackList;
            set => base.StackList = value;
        }


        public override void SetStackHolder(Transform otherTransform)
        {
            otherTransform.SetParent(transform);
        }

        public override void GetStack(GameObject stackableObj)
        {
            GetStackSequence = DOTween.Sequence();
            var randomBouncePosition = CalculateRandomAddStackPosition();
            var randomRotation = CalculateRandomStackRotation();

            GetStackSequence.Append(stackableObj.transform.DOLocalMove(randomBouncePosition, .5f));
            GetStackSequence.Join(stackableObj.transform.DOLocalRotate(randomRotation, .5f)).OnComplete(() =>
            {
                stackableObj.transform.rotation = Quaternion.LookRotation(transform.forward);

                StackLists.Add(stackableObj);

                stackableObj.transform.DOLocalMove(positionList[StackLists.Count - 1], 0.3f);
            });

        }

        public void OnRemoveAllStack()
        {
            if (!canRemove)
                return;

            canRemove = false;

            stackListConstCount = StackLists.Count;

            RemoveAllStack();
        }

        private async void RemoveAllStack()
        {

            if (StackLists.Count == 0)
            {
                canRemove = true;
                return;
            }

            if (StackLists.Count >= stackListConstCount - 6)
            {
                RemoveStackAnimation(StackLists[StackLists.Count - 1]);
                StackLists.TrimExcess();
                await Task.Delay(201);
                RemoveAllStack();
            }
            else
            {
                for (int i = 0; i < StackLists.Count; i++)
                {
                    RemoveStackAnimation(StackLists[i]);
                    StackLists.TrimExcess();
                }

            }
        }

        private void RemoveStackAnimation(GameObject removedStack)
        {
            GetStackSequence = DOTween.Sequence();
            var randomRemovedStackPosition = CalculateRandomRemoveStackPosition();
            var randomRemovedStackRotation = CalculateRandomStackRotation();

            GetStackSequence.Append(removedStack.transform.DOLocalMove(randomRemovedStackPosition, .2f));
            GetStackSequence.Join(removedStack.transform.DOLocalRotate(randomRemovedStackRotation, .2f)).OnComplete(() =>
            {
                removedStack.transform.rotation = Quaternion.LookRotation(transform.forward);


                StackLists.Remove(removedStack);
                removedStack.transform.DOLocalMove(transform.localPosition, .1f).OnComplete(() =>
                {
                    removedStack.transform.SetParent(null);
                    removedStack.SetActive(false);
                });


            });
        }

        public override void ResetStack(IStackable stackable)
        {
            base.ResetStack(stackable);
        }

        public void GetStackPositions(List<Vector3> stackPositions)
        {
            positionList = stackPositions;

        }

        private Vector3 CalculateRandomAddStackPosition()
        {
            var randomHeight = Random.Range(0.3f, 1f);
            var randomAngle = Random.Range(180, 360);
            var rad = randomAngle * Mathf.Deg2Rad;
            return new Vector3(radiusAround * Mathf.Cos(rad),
                transform.parent.position.y + randomHeight, -radiusAround * Mathf.Sin(rad));
        }
        private Vector3 CalculateRandomRemoveStackPosition()
        {
            var randomHeight = Random.Range(0.1f, 3f);
            var randomAngle = Random.Range(1, 179);
            var rad = randomAngle * Mathf.Deg2Rad;
            return new Vector3(radiusAround * Mathf.Cos(rad),
                transform.parent.position.y + randomHeight, radiusAround * Mathf.Sin(rad));
        }

        private Vector3 CalculateRandomStackRotation()
        {
            var randomRotationX = Random.Range(-90, 90);
            var randomRotationY = Random.Range(-90, 90);
            var randomRotationZ = Random.Range(-90, 90);
            return new Vector3(randomRotationX, randomRotationY, randomRotationZ);
        }



    }
}