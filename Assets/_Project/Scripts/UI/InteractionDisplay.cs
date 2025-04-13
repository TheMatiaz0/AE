using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using UnityEngine;

namespace AE
{
    public class InteractionDisplay : MonoBehaviour
    {
        [SerializeField] private PlayerContext context;
        [SerializeField] private RectTransform indicator;

        [Header("Start Settings")]
        [SerializeField] private float startDuration = 1.2f;
        [SerializeField] private Ease startEase;
        [Header("Destination Settings")]
        [SerializeField] private Vector2 endPosition;
        [SerializeField] private float endDuration = 2f;
        [SerializeField] private Ease endEase;

        private Vector2 startPosition;
        private Tween startTween;
        private Tween endTween;

        private void Awake()
        {
            startPosition = indicator.anchoredPosition;
            context.InteractionController.OnTargetChanged += OnInteractionAvailable;
        }

        private void OnInteractionAvailable(IInteractable interactable)
        {
            if (interactable != null)
            {
                if (startTween.IsActive())
                {
                    return;
                }
                if (endTween.IsActive())
                {
                    endTween.Kill();
                }

                EnableObject();
                startTween = indicator.DOAnchorPos(endPosition, endDuration).SetEase(startEase);
            }
            else
            {
                if (endTween.IsActive())
                {
                    return;
                }
                if (startTween.IsActive())
                {
                    startTween.Kill();
                }

                endTween = indicator.DOAnchorPos(startPosition, startDuration).SetEase(endEase).OnComplete(DisableObject);
            }
        }

        private void EnableObject()
        {
            indicator.gameObject.SetActive(true);
        }

        private void DisableObject()
        {
            indicator.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            context.InteractionController.OnTargetChanged -= OnInteractionAvailable;
        }
    }
}
