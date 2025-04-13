namespace AE
{
    public interface IInteractable
    {
        bool IsInteractable { get; }
        InteractablePrompt InteractionPrompt { get; }
        void Interact(IInteractionContext context);
    }
}
