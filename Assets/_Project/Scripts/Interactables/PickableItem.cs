using System;
using UnityEngine;

namespace AE
{
    public class PickableItem : MonoBehaviour, IInteractable
    {
        public event Action OnComplete;
        public event Action OnUpdate;

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

        public ItemReference ItemData => itemData;
        public Vector3 HeldPosition => heldPosition;
        public Vector3 HeldRotation => heldRotation;

        public bool IsInteractable { get; private set; } = true;

        public InteractablePrompt GetInteractionPrompt()
        {
            return prompt;
        }

        public void Interact(IInteractionContext context)
        {
            Pickup(context.InteractionController);
            OnComplete?.Invoke();
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
            OnUpdate?.Invoke();
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
