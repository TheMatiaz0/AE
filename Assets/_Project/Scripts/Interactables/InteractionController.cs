using DG.Tweening;
using System;
using UnityEngine;

namespace AE
{
    public class InteractionController : MonoBehaviour
    {
        private const string DefaultLayerMask = "Default";

        public event Action<IInteractable> OnTargetChanged;

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

        private IInteractable currentTarget;
        private PickableItem heldItem;
        private InteractionContext context;
        private Sequence dropSequence;
        private Sequence pickupSequence;

        public PickableItem HeldItem => heldItem;
        public IInteractable CurrentTarget => currentTarget;

        private void Awake()
        {
            context = new InteractionContext(this);
        }

        private void Update()
        {
            CheckForInteractables();
        }

        private void CheckForInteractables()
        {
            var ray = playerCamera.ViewportPointToRay(new(0.5f, 0.5f, 0));

            if (Physics.Raycast(ray, out var hit, interactionRange, interactableLayer))
            {
                if ((hit.transform.parent != null 
                    && hit.collider.transform.parent.TryGetComponent<IInteractable>(out var interactable)) 
                    || hit.collider.TryGetComponent(out interactable))
                {
                    currentTarget = interactable;
                    OnTargetChanged?.Invoke(interactable);
                }
                else
                {
                    currentTarget = null;
                    OnTargetChanged?.Invoke(null);
                }
            }
            else
            {
                currentTarget = null;
                OnTargetChanged?.Invoke(null);
            }
        }

        public void AssignItemToHand(PickableItem item)
        {
            if (pickupSequence.IsActive() || heldItem != null)
            {
                return;
            }

            dropSequence?.Kill();
            heldItem = item;
            item.transform.SetParent(itemHolder);

            pickupSequence = CreatePickupSequence(item);
        }

        private Sequence CreatePickupSequence(PickableItem item)
        {
            var sequence = DOTween.Sequence();

            if (item == null || item.transform == null)
            {
                Debug.LogWarning("Attempted to create pickup sequence for null item");
                return sequence;
            }

            sequence.SetLink(item.gameObject, LinkBehaviour.KillOnDestroy).SetTarget(item.gameObject).SetEase(pickupEaseType);

            sequence.Insert(0, item.transform.DOLocalMove(item.HeldPosition, pickupAnimationDuration));
            sequence.Join(item.transform.DOLocalRotate(item.HeldRotation, pickupAnimationDuration));

            return sequence;
        }

        public void ConsumeHeldItem()
        {
            if (heldItem != null)
            {
                Destroy(heldItem.gameObject);
            }
        }

        private void DropItem()
        {
            if (heldItem != null)
            {
                pickupSequence?.Kill();
                heldItem.transform.SetParent(null);

                var droppedItem = heldItem;
                heldItem = null;

                dropSequence = CreateDropSequence(droppedItem, GetDropPosition());
            }
        }

        private Vector3 GetDropPosition()
        {
            Vector3 forwardDrop = transform.position + transform.forward;
            Vector3 initialDropPos = forwardDrop + Vector3.up * 1f;

            Vector3 dropPosition;

            if (Physics.Raycast(initialDropPos, Vector3.down, out var hit, 5f, LayerMask.GetMask(DefaultLayerMask)))
            {
                dropPosition = hit.point + Vector3.up * 0.05f;
            }
            else
            {
                Debug.LogWarning("No ground detected while dropping. Dropping at fallback position.");
                dropPosition = transform.position + transform.forward * 0.3f + Vector3.down * 1.5f;
            }

            return dropPosition;
        }

        private Sequence CreateDropSequence(PickableItem item, Vector3 dropPosition)
        {
            var lookDirection = -transform.forward;
            lookDirection.y = 0f;

            var targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up).eulerAngles;

            var dropSequence = DOTween.Sequence();

            dropSequence.Join(item.transform.DOJump(dropPosition, 1, 2, dropAnimationDuration).SetEase(dropEaseType));
            dropSequence.Join(item.transform.DORotate(targetRotation, dropAnimationDuration).SetEase(dropEaseType))
                .SetLink(this.gameObject);

            dropSequence.OnComplete(() =>
            {
                item.Drop();
            });

            return dropSequence;
        }

        public void OnInteract()
        {
            if (currentTarget != null && currentTarget.IsInteractable)
            {
                currentTarget.Interact(context);
            }
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
