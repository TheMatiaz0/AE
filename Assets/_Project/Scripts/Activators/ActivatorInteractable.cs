using System;
using UnityEngine;

namespace AE
{
    public class ActivatorInteractable : MonoBehaviour, IInteractable
    {
        public event Action OnComplete;
        public event Action OnUpdate;

        [SerializeField] private InteractablePrompt prompt;
        [SerializeReference, SubclassSelector] private IActivable result;

        public bool IsInteractable { get; private set; }

        public InteractablePrompt GetInteractionPrompt() => prompt;

        public void Interact(IInteractionContext context)
        {
            result?.Activate(context);

            IsInteractable = false;
            OnComplete?.Invoke();
        }
    }
}
