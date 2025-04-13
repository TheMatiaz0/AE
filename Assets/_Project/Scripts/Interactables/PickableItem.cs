using System;
using System.Collections;
using UnityEngine;

namespace AE
{
    public class PickableItem : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private ItemReference itemData;
        [SerializeField]
        private InteractablePrompt prompt;
        [SerializeField]
        private Vector3 heldPosition;
        [SerializeField]
        private Vector3 heldRotation;
        [SerializeField]
        private Collider activeCollider;

        public ItemReference ItemReference => itemData;
        public Vector3 HeldPosition => heldPosition;
        public Vector3 HeldRotation => heldRotation;
        public InteractablePrompt InteractionPrompt => prompt;

        public bool IsInteractable { get; private set; } = true;

        public void Interact(IInteractionContext context)
        {
            if (context.InteractionController != null)
            {
                Pickup(context.InteractionController);
            }
        }

        private void Pickup(InteractionController controller)
        {
            IsInteractable = false;
            activeCollider.enabled = false;
            controller.AssignItemToHand(this);
        }

        public void Drop()
        {
            IsInteractable = true;
            activeCollider.enabled = true;
        }

#if UNITY_EDITOR

        private void OnValidate()
        {
            if (activeCollider == null)
            {
                activeCollider = GetComponent<Collider>(); 
            }
        }

#endif
    }
}
