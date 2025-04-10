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

        [Header("Camera Rotation Sway")]
        [SerializeField] private bool enableCameraRotationSway = true;
        [SerializeField] private float rotationSwayAmount = 2.0f;
        [SerializeField] private float rotationSwayLimit = 5.0f;
        [SerializeField] private float rotationSwaySmoothness = 4.0f;
        [SerializeField] private bool inverseRotationSway = false;

        [Header("Camera Movement Sway")]
        [SerializeField] private bool enableMovementSway = true;
        [SerializeField] private float movementSwayAmount = 1.0f;
        [SerializeField] private float movementSwayLimit = 3.0f;
        [SerializeField] private float movementSwaySmoothness = 3.0f;
        [SerializeField] private bool inverseMovementSway = false;

        [Header("Headbob Settings")]
        [SerializeField] private bool enableHeadbob = true;
        [SerializeField] private float bobAmplitude = 0.015f;
        [SerializeField] private float bobFrequency = 10.0f;
        [SerializeField] private float headbobSmoothing = 6.0f;
        [SerializeField] private float headbobTiltAmount = 1.5f;
        [SerializeField] private float headbobTiltSpeed = 4f;
        [SerializeField] private AnimationCurve bobCurve;

        [Header("Breathing Settings")]
        [SerializeField] private bool enableBreathing = true;
        [SerializeField] private float breatheSpeed = 1.0f;
        [SerializeField] private float breatheAmplitude = 0.0015f;
        [SerializeField] private float breathePitchAmount = 0.5f;
        [SerializeField] private float breatheTiltAmount = 0.2f;

        private CharacterController characterController;
        private Camera playerCamera;
        private Vector3 moveDirection;
        private float rotationX;
        private Vector2 inputMovement;
        private Vector2 mouseInput;

        private float bobTimer;
        private float bobTiltTimer;
        private float breatheTimer;
        private Vector3 currentCameraOffset;
        private Vector3 targetCameraOffset;
        private Vector3 initialCameraLocalPosition;
        private Quaternion initialCameraLocalRotation;
        private float velocityMagnitude;

        private Vector2 previousMouseInput;
        private Vector2 currentMouseDelta;
        private Vector3 rotationSwayAngles;
        private Vector3 movementSwayAngles;
        private Vector3 headbobAngles;
        private Vector3 breathingAngles;

        private void Start()
        {
            initialCameraLocalPosition = playerCamera.transform.localPosition;
            initialCameraLocalRotation = playerCamera.transform.localRotation;

            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            velocityMagnitude = new Vector2(characterController.velocity.x, characterController.velocity.z).magnitude;

            CalculateMovement();
            CalculateGravity();
            ApplyMovement();

            UpdateTimers();
        }

        private void LateUpdate()
        {
            CalculateMouseDelta();

            ApplyCameraRotation();

            ApplyRotationSway();
            ApplyMovementSway();
            ApplyHeadbob();
            ApplyBreathing();

            SmoothenCameraEffects();
        }

        private void CalculateMouseDelta()
        {
            currentMouseDelta = mouseInput - previousMouseInput;
            previousMouseInput = mouseInput;
        }

        private void UpdateTimers()
        {
            if (characterController.isGrounded && velocityMagnitude > 0.1f)
            {
                bobTimer += Time.deltaTime * velocityMagnitude * bobFrequency;
                if (bobTimer > 1f)
                {
                    bobTimer -= 1f;
                }

                bobTiltTimer += Time.deltaTime * headbobTiltSpeed;
                if (bobTiltTimer > Mathf.PI * 2)
                {
                    bobTiltTimer -= Mathf.PI * 2;
                }
            }

            breatheTimer += Time.deltaTime * breatheSpeed;
            if (breatheTimer > Mathf.PI * 2)
            {
                breatheTimer -= Mathf.PI * 2;
            }
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
                var scaledMouseInput = mouseInput * mouseSensitivity;

                rotationX -= scaledMouseInput.y;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

                cameraHolder.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, scaledMouseInput.x, 0);
            }
        }

        private void ApplyRotationSway()
        {
            if (!enableCameraRotationSway || currentMouseDelta.magnitude < 0.01f)
            {
                rotationSwayAngles = Vector3.Lerp(
                    rotationSwayAngles,
                    Vector3.zero,
                    Time.deltaTime * rotationSwaySmoothness
                );

                return;
            }

            var swayYaw = -currentMouseDelta.x * rotationSwayAmount * (inverseRotationSway ? -1f : 1f);
            var swayPitch = -currentMouseDelta.y * rotationSwayAmount * (inverseRotationSway ? -1f : 1f);
            var swayRoll = -currentMouseDelta.x * rotationSwayAmount * 0.5f * (inverseRotationSway ? -1f : 1f);

            swayYaw = Mathf.Clamp(swayYaw, -rotationSwayLimit, rotationSwayLimit);
            swayPitch = Mathf.Clamp(swayPitch, -rotationSwayLimit, rotationSwayLimit);
            swayRoll = Mathf.Clamp(swayRoll, -rotationSwayLimit, rotationSwayLimit);

            rotationSwayAngles = Vector3.Lerp(
                rotationSwayAngles,
                new Vector3(swayPitch, swayYaw, swayRoll),
                Time.deltaTime * rotationSwaySmoothness
            );
        }

        private void ApplyMovementSway()
        {
            if (!enableMovementSway || velocityMagnitude < 0.01f || !characterController.isGrounded)
            {
                movementSwayAngles = Vector3.Lerp(
                    movementSwayAngles,
                    Vector3.zero,
                    Time.deltaTime * movementSwaySmoothness
                );

                return;
            }

            var tiltRoll = -inputMovement.x * movementSwayAmount * (inverseMovementSway ? -1f : 1f);
            var tiltAmount = inputMovement.y * movementSwayAmount * 0.5f * (inverseMovementSway ? -1f : 1f);

            tiltRoll = Mathf.Clamp(tiltRoll, -movementSwayLimit, movementSwayLimit);
            tiltAmount = Mathf.Clamp(tiltAmount, -movementSwayLimit * 0.5f, movementSwayLimit * 0.5f);

            movementSwayAngles = Vector3.Lerp(
                movementSwayAngles,
                new Vector3(tiltAmount, 0, tiltRoll),
                Time.deltaTime * movementSwaySmoothness
            );
        }

        private void ApplyHeadbob()
        {
            if (!enableHeadbob || velocityMagnitude < 0.1f || !characterController.isGrounded)
            {
                targetCameraOffset.y = Mathf.Lerp(targetCameraOffset.y, 0f, Time.deltaTime * headbobSmoothing);
                targetCameraOffset.x = Mathf.Lerp(targetCameraOffset.x, 0f, Time.deltaTime * headbobSmoothing);
                headbobAngles = Vector3.Lerp(headbobAngles, Vector3.zero, Time.deltaTime * headbobSmoothing);
                return;
            }

            var bobPitch = bobCurve.Evaluate(bobTimer) * bobAmplitude * velocityMagnitude;
            var bobEffectX = bobCurve.Evaluate(bobTimer + 0.5f) * bobAmplitude * 0.5f * velocityMagnitude;

            var bobRoll = bobCurve.Evaluate(bobTimer + 0.25f) * Mathf.Sin(bobTiltTimer) * headbobTiltAmount * velocityMagnitude;

            targetCameraOffset.y = bobPitch;
            targetCameraOffset.x = bobEffectX;

            headbobAngles = Vector3.Lerp(
                headbobAngles,
                new Vector3(bobPitch * 2.0f, 0, bobRoll),
                Time.deltaTime * headbobSmoothing
            );
        }

        private void ApplyBreathing()
        {
            if (!enableBreathing)
            {
                breathingAngles = Vector3.Lerp(breathingAngles, Vector3.zero, Time.deltaTime * 2f);
                return;
            }

            var breatheIntensity = 1.0f - Mathf.Clamp01(velocityMagnitude / 2.0f);
            var breatheEffect = Mathf.Sin(breatheTimer) * breatheAmplitude * breatheIntensity;

            targetCameraOffset.y += breatheEffect;

            var breathePitch = Mathf.Sin(breatheTimer) * breathePitchAmount * 0.01f * breatheIntensity;
            var breatheRoll = Mathf.Sin(breatheTimer + 0.5f) * breatheTiltAmount * 0.01f * breatheIntensity;

            breathingAngles = Vector3.Lerp(
                breathingAngles,
                new(breathePitch, 0, breatheRoll),
                Time.deltaTime * 2f
            );
        }

        private void SmoothenCameraEffects()
        {
            currentCameraOffset = Vector3.Lerp(
                currentCameraOffset,
                targetCameraOffset,
                Time.deltaTime * headbobSmoothing
            );

            playerCamera.transform.localPosition = initialCameraLocalPosition + currentCameraOffset;

            var combinedRotation = Quaternion.Euler(
                rotationSwayAngles +
                movementSwayAngles +
                headbobAngles +
                breathingAngles
            );

            playerCamera.transform.localRotation = Quaternion.Slerp(
                playerCamera.transform.localRotation,
                initialCameraLocalRotation * combinedRotation,
                Time.deltaTime * Mathf.Min(rotationSwaySmoothness, movementSwaySmoothness)
            );
        }

        public void OnMove(InputValue ctx)
        {
            inputMovement = ctx.Get<Vector2>();
        }

        public void OnLook(InputValue ctx)
        {
            mouseInput = ctx.Get<Vector2>();
        }

        private void ApplyMovement()
        {
            characterController.Move(moveDirection * Time.deltaTime);
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