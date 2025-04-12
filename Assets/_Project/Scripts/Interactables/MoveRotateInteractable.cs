using DG.Tweening;
using System;
using UnityEngine;

namespace AE
{
    public class MoveRotateInteractable : MonoBehaviour, IInteractable
    {
        public event Action OnComplete;
        public event Action OnUpdate;

        [SerializeField] private Transform element;
        [SerializeField] private InteractablePrompt prompt;

        [Header("Destination Settings")]
        [SerializeField] private Vector3 endPosition;
        [SerializeField] private Vector3 endRotation;
        [SerializeField] private float endDuration;
        [SerializeField] private Ease endEase;

        public bool IsInteractable { get; private set; }

        public InteractablePrompt GetInteractionPrompt() => prompt;

        public void Interact(IInteractionContext context)
        {
            element.DOLocalMove(endPosition, endDuration)
                .SetEase(endEase);
            element.DOLocalRotate(endRotation, endDuration)
                .SetEase(endEase);

            IsInteractable = false;
            OnComplete?.Invoke();
        }
    }
}
