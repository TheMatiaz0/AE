using System;
using System.Collections.Generic;
using UnityEngine;

namespace AE
{
    public class ItemSocketInteractable : MonoBehaviour, IInteractable
    {
        public event Action OnComplete;
        public event Action OnUpdate;

        [SerializeField] private InteractablePrompt prompt;
        [Header("Condition")]
        [SerializeField] private ItemReference requiredItem;
        [SerializeField] private int maxItemCount = 1;
        [Header("Result")]
        [SerializeField] private List<GameObject> visualSlots;
        [SerializeField] private bool shouldActivateObjects = true;

        private readonly List<ItemReference> insertedItems;

        public bool IsInteractable => insertedItems.Count < maxItemCount;

        public InteractablePrompt GetInteractionPrompt() => prompt;

        public void Interact(IInteractionContext context)
        {
            var controller = context.InteractionController;

            if (controller.HeldItem == null || !IsInteractable || controller.HeldItem.ItemData != requiredItem)
            {
                return;
            }

            insertedItems.Add(controller.HeldItem.ItemData);
            controller.ConsumeItem();

            UpdateVisuals(insertedItems.Count - 1);
        }

        private void UpdateVisuals(int index)
        {
            if (index < visualSlots.Count)
            {
                visualSlots[index].SetActive(shouldActivateObjects);
                OnUpdate?.Invoke();
            }
            else
            {
                OnComplete?.Invoke();
            }
        }
    }
}
