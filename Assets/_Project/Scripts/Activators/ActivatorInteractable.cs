using System;
using UnityEngine;

namespace AE
{
    public class ActivatorInteractable : MonoBehaviour, IInteractable
    {
        public event Action OnComplete;
        public event Action OnUpdate;

        [SerializeField] private InteractablePrompt prompt;
        [SerializeReference, SubclassSelector] private ICondition conditions;
        [SerializeReference, SubclassSelector] private IActivable activateEffects;
        [SerializeReference, SubclassSelector] private IActivable deactivateEffects;

        public bool IsInteractable { get; private set; } = true;

        public InteractablePrompt GetInteractionPrompt() => prompt;

        public void Interact(IInteractionContext context)
        {
            activateEffects?.Activate(context);
            deactivateEffects?.Deactivate(context);

            IsInteractable = false;
            OnComplete?.Invoke();
        }
    }
}
