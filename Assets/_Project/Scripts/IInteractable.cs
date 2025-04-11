using UnityEngine;

namespace AE
{
    public interface IInteractable
    {
        InteractablePrompt GetInteractionPrompt();
        void Interact(InteractionController controller);
    }
}
