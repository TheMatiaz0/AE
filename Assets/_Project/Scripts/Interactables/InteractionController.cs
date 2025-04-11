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

        [Header("Item Hold Settings")]
        [SerializeField] private Transform itemHolder;
        [SerializeField] private float pickupAnimationDuration = 0.2f;
        [SerializeField] private Ease pickupEaseType = Ease.OutBack;

        [Header("Item Drop Settings")]
        [SerializeField] private float dropAnimationDuration = 0.3f;
        [SerializeField] private Ease dropEaseType = Ease.InCubic;
        [SerializeField] private float dropForwardOffset = 0.3f;
        [SerializeField] private float dropVerticalOffset = -1.8f;

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

            var seq = DOTween.Sequence();
            seq.Append(item.transform.DOLocalMove(item.HeldPosition, pickupAnimationDuration).SetEase(pickupEaseType));
            seq.Join(item.transform.DOLocalRotate(item.HeldRotation, pickupAnimationDuration).SetEase(pickupEaseType));
        }

        private void DropItem()
        {
            if (heldItem != null)
            {
                heldItem.transform.SetParent(null);

                heldItem.transform.GetPositionAndRotation(out var startPosition, out var startRotation);

                var dropPosition = transform.position + transform.forward * dropForwardOffset;
                dropPosition.y = transform.position.y + dropVerticalOffset;

                // TODO: do raycast check of ground

                var droppedItem = heldItem;
                heldItem = null;

                var lookDirection = -transform.forward;
                lookDirection.y = 0f;

                var targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);

                var dropSequence = DOTween.Sequence();

                dropSequence.Join(droppedItem.transform.DOMove(dropPosition, dropAnimationDuration).SetEase(dropEaseType));
                dropSequence.Join(droppedItem.transform.DORotateQuaternion(targetRotation, dropAnimationDuration).SetEase(dropEaseType));

                dropSequence.OnComplete(() =>
                {
                    droppedItem.Drop();
                });
            }
        }

        private void OnDestroy()
        {
            DOTween.KillAll();
        }

        public void OnInteract()
        {
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
