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
        private Sequence sequence;

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

            sequence?.Kill();
        }

        public async UniTask ActivateAsync(IContext context)
        {
            if (sequence.IsActive())
            {
                sequence.Kill();
            }

            if (element == null || element.Equals(null))
            {
                Debug.LogWarning("MoveRotateActivable skipped due to missing element.");
                return;
            }

            if (startPosition == null)
            {
                startPosition = element.localPosition;
            }
            if (startRotation == null)
            {
                startRotation = element.localRotation.eulerAngles;
            }

            var elementGameObject = element.gameObject;

            sequence = DOTween.Sequence();

            _ = sequence.SetEase(endEase).SetLink(elementGameObject, LinkBehaviour.KillOnDestroy).SetTarget(elementGameObject);

            _ = sequence.Insert(0f, element.DOLocalMove(endPosition, endDuration)).SetTarget(elementGameObject);
            _ = sequence.Join(element.DOLocalRotate(endRotation, endDuration)).SetTarget(elementGameObject);

            await sequence.AsyncWaitForCompletion();
        }

        public UniTask DeactivateAsync(IContext context)
        {
            Deactivate(context);
            return UniTask.CompletedTask;
        }
    }
}
