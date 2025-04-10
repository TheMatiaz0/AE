using UnityEngine;
using UnityEngine.InputSystem;

namespace AE
{
    [RequireComponent(typeof(CharacterController))]
    public class HorrorFPSController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float walkSpeed = 2.5f;
        [SerializeField] private float gravityMultiplier = 2.0f;

        [Header("Mouse Look Settings")]
        [SerializeField] private float mouseSensitivity = 2.0f;
        [SerializeField] private float lookXLimit = 80.0f;
        [SerializeField] private Transform cameraHolder;

        private CharacterController characterController;
        private Camera playerCamera;
        private Vector3 moveDirection = Vector3.zero;
        private float rotationX = 0;
        private Vector2 inputMovement = Vector2.zero;
        private Vector2 mouseInput = Vector2.zero;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            if (moveDirection == Vector3.zero)
            {
                return;
            }

            CalculateMovement();
            CalculateGravity();
            ApplyMovement();
        }

        private void LateUpdate()
        {
            ApplyCameraRotation();
        }

        private void CalculateMovement()
        {
            if (inputMovement != Vector2.zero)
            {
                var forward = transform.forward;
                var right = transform.right;

                forward.y = 0;
                right.y = 0;
                forward.Normalize();
                right.Normalize();

                var move = forward * inputMovement.y + right * inputMovement.x;
                move.Normalize();
                move *= walkSpeed;

                moveDirection.x = move.x;
                moveDirection.z = move.z;
            }
            else
            {
                moveDirection.x = 0;
                moveDirection.z = 0;
            }
        }

        private void CalculateGravity()
        {
            if (characterController.isGrounded)
            {
                moveDirection.y = -0.5f;
            }
            else
            {
                moveDirection.y += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
            }
        }

        private void ApplyCameraRotation()
        {
            if (mouseInput != Vector2.zero)
            {
                Vector2 scaledMouseInput = mouseInput * mouseSensitivity;

                rotationX -= scaledMouseInput.y;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

                cameraHolder.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, scaledMouseInput.x, 0);
            }
        }

        private void ApplyMovement()
        {
            characterController.Move(moveDirection * Time.deltaTime);
        }

        public void OnMove(InputValue ctx)
        {
            inputMovement = ctx.Get<Vector2>();
        }

        public void OnLook(InputValue ctx)
        {
            mouseInput = ctx.Get<Vector2>();
        }

#if UNITY_EDITOR

        private void OnValidate()
        {
            if (characterController == null)
            {
                characterController = GetComponent<CharacterController>();
            }

            if (cameraHolder == null)
            {
                cameraHolder = this.transform.GetChild(0);
            }

            if (playerCamera == null)
            {
                playerCamera = cameraHolder.GetComponentInChildren<Camera>();
            }
        }

#endif
    }
}