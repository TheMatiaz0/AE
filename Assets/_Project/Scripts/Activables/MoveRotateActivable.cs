using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;

namespace AE
{
    [Serializable]
    public class MoveRotateActivable : IAsyncActivable
    {
        [SerializeField]
        private Transform element;

        [Header("Start Settings")]
        [SerializeField] private Vector3 startPosition;
        [SerializeField] private Vector3 startRotation;

        [Header("Destination Settings")]
        [SerializeField] private Vector3 endPosition;
        [SerializeField] private Vector3 endRotation;
        [SerializeField] private float endDuration;
        [SerializeField] private Ease endEase;

        public void Activate(IContext context)
        {
            _ = ActivateAsync(context);
        }

        public void Deactivate(IContext context)
        {
            element.localPosition = startPosition;
            element.localEulerAngles = startRotation;
        }

        public async UniTask ActivateAsync(IContext context)
        {
            Sequence sequence = DOTween.Sequence();

            await sequence.Insert(0, element.DOLocalMove(endPosition, endDuration)).AsyncWaitForCompletion();
            await sequence.Insert(0, element.DOLocalRotate(endRotation, endDuration))
                .SetEase(endEase).AsyncWaitForCompletion();
        }

        public UniTask DeactivateAsync(IContext context)
        {
            Deactivate(context);
            return UniTask.CompletedTask;
        }
    }
}
