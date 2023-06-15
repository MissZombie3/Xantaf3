using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        static private readonly int Velocity = Animator.StringToHash("Velocity");
    
        [SerializeField] private Animator animator = default;

        private PlayerMovement playerMovement = default;

        private void Awake()
        {
            playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            animator.SetFloat(Velocity, playerMovement.Velocity, 0.2F, Time.deltaTime);
        }
    }
}
