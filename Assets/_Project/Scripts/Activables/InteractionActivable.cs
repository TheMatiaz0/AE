using System;
using System.Collections.Generic;
using UnityEngine;

namespace AE
{
    [Serializable]
    public class InteractionActivable : IActivable
    {
        [Header("Activate - Assign Item, Deactivate - Consume Item")]
        [SerializeField] private PickableItem item;
        [SerializeField] private int requiredAmount = 1;
        [SerializeReference, SubclassSelector] private IActivable onCollectedRequiredAmount;

        private readonly List<ItemReference> collectedItems = new();

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
                collectedItems.Add(interaction.InteractionController.HeldItem.ItemReference);
                interaction.InteractionController.ConsumeHeldItem();

                if (collectedItems.Count >= requiredAmount)
                {
                    onCollectedRequiredAmount?.Activate(context);
                    interaction.InteractionController.CurrentTarget.IsInteractable = false;
                }
            }
        }
    }
}
