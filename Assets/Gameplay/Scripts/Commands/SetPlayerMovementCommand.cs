using Fungus;
using Gameplay.Player;
using UnityEngine;

namespace Gameplay.Commands
{
    [CommandInfo("Gameplay", "Set Player Movement", "Enable or disable player movement.")]
    public class SetPlayerMovementCommand : Command
    {
        [SerializeField] private bool isEnabled = true;
    
        private PlayerMovement playerMovement;

        private void Awake()
        {
            playerMovement = FindObjectOfType<PlayerMovement>();
        }

        override public void OnEnter()
        {
            playerMovement.IsDisabledByFungus = !isEnabled;
            Continue();
        }
    }
}