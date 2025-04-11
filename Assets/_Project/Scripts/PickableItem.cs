using UnityEngine;

namespace AE
{
    public class PickableItem : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private InteractablePrompt prompt;
        [SerializeField]
        private Vector3 heldRotation;
        [SerializeField]
        private Collider activeCollider;

        public Vector3 HeldRotation => heldRotation;

        public bool CanBePickedUp { get; private set; } = true;

        public InteractablePrompt GetInteractionPrompt()
        {
            return prompt;
        }

        public void Interact(InteractionController controller)
        {
            if (CanBePickedUp)
            {
                Pickup(controller);
            }
        }

        private void Pickup(InteractionController controller)
        {
            CanBePickedUp = false;
            activeCollider.enabled = false;
            controller.AssignItemToHand(this);
        }

        public void Drop()
        {
            CanBePickedUp = true;
            activeCollider.enabled = true;
        }
    }
}
