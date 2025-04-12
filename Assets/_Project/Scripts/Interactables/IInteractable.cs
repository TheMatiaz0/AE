namespace AE
{
    public interface IInteractable
    {
        bool IsInteractable { get; }
        InteractablePrompt GetInteractionPrompt();
        void Interact(IInteractionContext context);
    }
}
