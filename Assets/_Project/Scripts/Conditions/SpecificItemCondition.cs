using System;
using UnityEngine;

namespace AE
{
    [Serializable]
    public class SpecificItemCondition : ICondition
    {
        [SerializeField] private ItemReference requiredItem;

        public bool IsConditionMet(IContext context)
        {
            if (context is IInteractionContext interaction)
            {
                if (interaction.InteractionController.HeldItem != null)
                {
                    return interaction.InteractionController.HeldItem.ItemReference == requiredItem;
                }
            }

            return false;
        }
    }
}
