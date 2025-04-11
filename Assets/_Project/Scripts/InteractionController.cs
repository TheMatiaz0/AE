using DG.Tweening;
using UnityEngine;

namespace AE
{
    public class InteractionController : MonoBehaviour
    {
        [SerializeField]
        private Camera playerCamera;

        [Header("Interaction Settings")]
        [SerializeField] private float interactionRange = 3f;
        [SerializeField] private LayerMask interactableLayer;

        [Header("Item Holding Settings")]
        [SerializeField] private Transform itemHolder;
        [SerializeField] private float pickupAnimationDuration = 0.2f;
        [SerializeField] private Ease pickupEaseType = Ease.OutBack;

        private IInteractable currentTarget;
        private PickableItem heldItem;

        private void Update()
        {
            CheckForInteractables();
        }

        private void CheckForInteractables()
        {
            var ray = playerCamera.ViewportPointToRay(new(0.5f, 0.5f, 0));

            if (Physics.Raycast(ray, out var hit, interactionRange, interactableLayer))
            {
                if (hit.collider.TryGetComponent<IInteractable>(out var interactable))
                {
                    Debug.Log($"{interactable.GetInteractionPrompt().name}");
                    currentTarget = interactable;
                }
                else
                {
                    currentTarget = null;
                }
            }
            else
            {
                currentTarget = null;
            }
        }

        public void AssignItemToHand(PickableItem item)
        {
            if (heldItem != null)
            {
                DropItem();
            }

            heldItem = item;

            item.transform.SetParent(itemHolder);

            item.transform.DOLocalMove(Vector3.zero, pickupAnimationDuration)
                .SetEase(pickupEaseType);
            item.transform.DOLocalRotate(item.HeldRotation, pickupAnimationDuration)
                .SetEase(pickupEaseType);
        }

        private void DropItem()
        {
            if (heldItem != null)
            {
                heldItem.transform.SetParent(null);

                var droppedItem = heldItem;
                heldItem = null;

                droppedItem.Drop();
            }
        }

        private void OnDestroy()
        {
            DOTween.KillAll();
        }

        public void OnInteract()
        {
            Debug.Log($"Is NULL: {currentTarget == null}");
            currentTarget?.Interact(this);
        }

        public void OnDrop()
        {
            DropItem();
        }

#if UNITY_EDITOR

        private void OnValidate()
        {
            if (playerCamera == null)
            {
                playerCamera = GetComponentInChildren<Camera>();
            }
        }

#endif
    }
}
