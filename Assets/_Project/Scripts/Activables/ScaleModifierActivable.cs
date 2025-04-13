using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;

namespace AE
{
    [Serializable]
    public class ScaleModifierActivable : IAsyncActivable
    {
        [SerializeField]
        private Transform element;

        [Header("Destination Settings")]
        [SerializeField] private Vector3 endScale;
        [SerializeField] private float endDuration;
        [SerializeField] private Ease endEase;

        private Vector3? startScale;
        private Tween tween;

        public void Activate(IContext context)
        {
            _ = ActivateAsync(context);
        }

        public void Deactivate(IContext context)
        {
            if (startScale != null)
            {
                element.localScale = startScale.Value;
            }

            tween?.Kill();
        }

        public async UniTask ActivateAsync(IContext context)
        {
            if (tween.IsActive())
            {
                tween.Kill();
            }

            if (element == null || element.Equals(null))
            {
                Debug.LogWarning("MoveRotateActivable skipped due to missing element.");
                return;
            }

            if (startScale == null)
            {
                startScale = element.localPosition;
            }

            var elementGameObject = element.gameObject;

            await (tween = element.DOScale(endScale, endDuration)
                .SetEase(endEase)
                .SetLink(elementGameObject, LinkBehaviour.KillOnDestroy)
                .SetTarget(elementGameObject)).AsyncWaitForCompletion();
        }

        public UniTask DeactivateAsync(IContext context)
        {
            Deactivate(context);
            return UniTask.CompletedTask;
        }
    }
}
