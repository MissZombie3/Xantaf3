using Fungus;
using Gameplay.Player;
using UnityEngine;

namespace Gameplay.Commands
{
    [CommandInfo("Gameplay", "Set Active Interactable", "Enable or disable player movement.")]
    public class SetInteractableCommand : Command
    {
        [SerializeField] private bool isEnabled = true;
    
        private Interactable interactable;

        private void Awake()
        {
            interactable = GetComponent<Interactable>();
        }

        override public void OnEnter()
        {
            interactable.enabled = isEnabled;
            Continue();
        }
    }
}