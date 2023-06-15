using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private LayerMask interactionLayer = default;
        [SerializeField] private float checkRate = 0.2F;
        [SerializeField] private float interactionDistance = 1.0F;
        [SerializeField] private float maxInteractionDistance = 4.5F;

        private float nextCheck = 0;
        private Interactable possibleInteractable = null;
        private PlayerMovement playerMovement;

        private void Awake()
        {
            playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            if (Time.time > nextCheck && !playerMovement.IsMovingAutomatic && !playerMovement.IsDisabledByFungus)
            {
                CheckInteractable();
                nextCheck = Time.time + checkRate;
            }

            if (playerMovement.IsDisabledByFungus && possibleInteractable != null)
                ClearInteractable();

            if (playerMovement.IsMovingAutomatic && possibleInteractable == null)
                playerMovement.StopAutomaticMovement();

            if (!playerMovement.IsMovingAutomatic && Mouse.current.leftButton.wasPressedThisFrame && possibleInteractable != null)
            {
                Vector3 myPosition = transform.position;
                myPosition.y = 0;
                Vector3 interactablePosition = possibleInteractable.transform.position;
                interactablePosition.y = 0;
                float distance = Vector3.Distance(myPosition, interactablePosition);
            
                if (distance <= interactionDistance)
                    possibleInteractable.Interact();
                else if (distance <= maxInteractionDistance)
                    playerMovement.MoveAutomaticTo(possibleInteractable.transform.position, () => possibleInteractable.Interact());
                else
                    Debug.LogWarning("El interactable esta muy lejos.");
            }
        }

        private void ClearInteractable()
        {
            possibleInteractable.SetHighlight(false);
            possibleInteractable = null;
        }

        private void CheckInteractable()
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit, 100, interactionLayer))
                TrySetInteractable(hit);
            else if (possibleInteractable != null)
                ClearInteractable();
        }

        private void TrySetInteractable(RaycastHit hit)
        {
            if (hit.collider.TryGetComponent(out Interactable interactable) && interactable.enabled)
            {
                possibleInteractable = interactable;
                possibleInteractable.SetHighlight(true);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position + Vector3.up, interactionDistance);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position + Vector3.up, maxInteractionDistance);
        }
    }
}