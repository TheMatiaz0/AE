using System;
using UnityEngine;

namespace AE
{
    [Serializable]
    public class InteractionActivable : IActivable
    {
        [SerializeField]
        private PickableItem item;

        public void Activate(IContext context)
        {
            if (context is IInteractionContext interaction)
            {
                interaction.InteractionController.AssignItemToHand(item);
            }
        }

        public void Deactivate(IContext context)
        {
            if (context is IInteractionContext interaction)
            {
                interaction.InteractionController.ConsumeHeldItem();
            }
        }
    }
}
