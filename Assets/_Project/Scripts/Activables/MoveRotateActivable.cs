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

        [Header("Destination Settings")]
        [SerializeField] private Vector3 endPosition;
        [SerializeField] private Vector3 endRotation;
        [SerializeField] private float endDuration;
        [SerializeField] private Ease endEase;

        [SerializeField]
        private bool shouldUseLocal = true;

        private Vector3? startPosition;
        private Vector3? startRotation;

        public void Activate(IContext context)
        {
            _ = ActivateAsync(context);
        }

        public void Deactivate(IContext context)
        {
            if (startPosition != null)
            {
                element.localPosition = startPosition.Value;
            }
            if (startRotation != null)
            {
                element.localEulerAngles = startRotation.Value;
            }

            DOTween.KillAll();
        }

        public async UniTask ActivateAsync(IContext context)
        {
            if (startPosition == null)
            {
                startPosition = element.localPosition;
            }
            if (startRotation == null)
            {
                startRotation = element.localRotation.eulerAngles;
            }

            var sequence = DOTween.Sequence();

            _ = sequence.Insert(0f, shouldUseLocal ? 
                element.DOLocalMove(endPosition, endDuration) : element.DOMove(endPosition, endDuration));
            _ = sequence.Insert(0f, shouldUseLocal ? 
                element.DOLocalRotate(endRotation, endDuration) : element.DORotate(endRotation, endDuration));
            _ = sequence.SetEase(endEase);

            await sequence.AsyncWaitForCompletion();
        }

        public UniTask DeactivateAsync(IContext context)
        {
            Deactivate(context);
            return UniTask.CompletedTask;
        }
    }
}
