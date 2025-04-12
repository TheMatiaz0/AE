using System;
using System.Collections.Generic;
using UnityEngine;

namespace AE
{
    public class ItemSocketInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private InteractablePrompt prompt;
        [Header("Condition")]
        [SerializeField] private ItemReference requiredItem;
        [SerializeField] private int maxItemCount = 1;
        [Header("Result")]
        [SerializeField] private List<GameObject> activateObjects;
        [SerializeField] private bool shouldActivateObjects = true;

        private readonly List<ItemReference> insertedItems;

        public bool IsInteractable => insertedItems.Count < maxItemCount;

        public InteractablePrompt GetInteractionPrompt() => prompt;

        public void Interact(IInteractionContext context)
        {
            var controller = context.InteractionController;

            if (controller.HeldItem == null || controller.HeldItem.ItemReference != requiredItem)
            {
                return;
            }

            insertedItems.Add(controller.HeldItem.ItemReference);
            controller.ConsumeHeldItem();

            UpdateVisuals(insertedItems.Count - 1);
        }

        private void UpdateVisuals(int index)
        {
            if (index < activateObjects.Count)
            {
                activateObjects[index].SetActive(shouldActivateObjects);
            }
        }
    }
}
