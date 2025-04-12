using UnityEngine;

namespace AE
{
    public class InteractionContext : IInteractionContext
    {
        public InteractionController InteractionController { get; }

        public InteractionContext(InteractionController controller) 
        {
            InteractionController = controller; 
        }
    }
}
