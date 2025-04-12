using System;

namespace AE
{
    public interface IInteractable
    {
        event Action OnComplete;
        event Action OnUpdate;

        bool IsInteractable { get; }
        InteractablePrompt GetInteractionPrompt();
        void Interact(IInteractionContext context);
    }
}
