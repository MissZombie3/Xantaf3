using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private InputActionReference movementInputReference = default;

        private PlayerMovement playerMovement;
        private InputAction movementCache;

        private void Awake()
        {
            playerMovement = GetComponent<PlayerMovement>();
            movementCache = movementInputReference.action;
        }

        private void OnEnable()
        {
            movementCache.Enable();
        }

        private void OnDisable()
        {
            movementCache.Disable();
        }

        private void Update()
        {
            playerMovement.MovementInput = movementCache.ReadValue<Vector2>();
        }
    }
}
