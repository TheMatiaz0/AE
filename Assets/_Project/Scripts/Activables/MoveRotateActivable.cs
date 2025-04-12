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

            Sequence sequence = DOTween.Sequence();

            await sequence.Insert(0f, element.DOLocalMove(endPosition, endDuration));
            await sequence.Insert(0f, element.DOLocalRotate(endRotation, endDuration));
            await sequence.SetEase(endEase);

            await sequence.AsyncWaitForCompletion();
        }

        public UniTask DeactivateAsync(IContext context)
        {
            Deactivate(context);
            return UniTask.CompletedTask;
        }
    }
}
