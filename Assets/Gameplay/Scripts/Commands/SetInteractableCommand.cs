using Fungus;
using Gameplay.Player;
using UnityEngine;

namespace Gameplay.Commands
{
    [CommandInfo("Gameplay", "Activar o desactivar interacción", "Activa o desactiva una interacción en específico.")]
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