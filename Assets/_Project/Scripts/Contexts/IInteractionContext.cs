namespace AE
{
    public interface IInteractionContext : IContext
    {
        InteractionController InteractionController { get; }
    }
}
