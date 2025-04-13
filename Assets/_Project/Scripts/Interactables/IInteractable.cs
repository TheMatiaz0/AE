namespace AE
{
    public interface IInteractable
    {
        bool IsInteractable { get; set; }
        InteractablePrompt InteractionPrompt { get; }
        void Interact(IInteractionContext context);
    }
}
