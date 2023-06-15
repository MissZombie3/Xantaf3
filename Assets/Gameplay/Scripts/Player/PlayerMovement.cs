using System;
using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField, Range(0, 1)] private float rotationSmooth = 0.05F;
        [SerializeField, Range(0, 1)] private float movementSmooth = 0.25F;
        [SerializeField] private float movementSpeed = 5;
        [SerializeField] private float gravityValue = -9.81F;

        private float currentRotationVelocity;
        private float currentMovementVelocity;
        private float currentSpeed;
        private float controllerVelocity;
        private float verticalSpeed;
        private CharacterController controller;
        private NavMeshAgent agent;
        private Action onDestinationReached;

        private Transform CameraTransform { get; set; }
    
        public Vector2 MovementInput { get; set; }
        public bool IsMovingAutomatic { get; private set; }
        public bool IsDisabledByFungus { get; set; }
        public float Velocity => IsMovingAutomatic ? agent.velocity.magnitude : controllerVelocity;
    
        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            agent = GetComponent<NavMeshAgent>();
            agent.isStopped = true;
            CameraTransform = Camera.main.transform;
        }

        private void Update()
        {
            agent.isStopped = !IsMovingAutomatic;
            if (IsMovingAutomatic)
            {
                if (agent.remainingDistance > 0.9F)
                    return;
                IsMovingAutomatic = false;
                onDestinationReached?.Invoke();
                onDestinationReached = null;
            }
            Vector3 moveDirection = (CameraTransform.right * MovementInput.x) + (CameraTransform.forward * MovementInput.y);
            moveDirection.y = 0;
            moveDirection.Normalize();
            ApplyRotation();
            currentSpeed = Mathf.SmoothDamp(currentSpeed, moveDirection.magnitude * movementSpeed, ref currentMovementVelocity, movementSmooth);
            controller.Move(transform.forward * currentSpeed * Time.deltaTime);
            controllerVelocity = controller.velocity.magnitude;
            ApplyGravity();
        }

        private void ApplyRotation()
        {
            if (MovementInput.magnitude == 0)
                return;
            float rotation = (Mathf.Atan2(MovementInput.x, MovementInput.y) * Mathf.Rad2Deg) + CameraTransform.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, rotation, ref currentRotationVelocity, rotationSmooth);
        }
    
        private void ApplyGravity()
        {
            verticalSpeed += gravityValue * Time.deltaTime;
            controller.Move(Vector3.up * verticalSpeed * Time.deltaTime);
        }

        public void MoveAutomaticTo(Vector3 destination, Action onReached)
        {
            agent.ResetPath();
            agent.destination = destination;
            onDestinationReached = onReached;
            IsMovingAutomatic = true;
        }

        public void StopAutomaticMovement()
        {
            agent.ResetPath();
            onDestinationReached = null;
            IsMovingAutomatic = false;
        }
    }
}