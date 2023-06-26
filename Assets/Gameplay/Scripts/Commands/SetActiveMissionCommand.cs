using Fungus;
using Gameplay.MissionsAndClues;
using UnityEngine;

namespace Gameplay.Commands
{
    [CommandInfo("Gameplay", "Activar Misión", "Activa o desactiva una misión en específico.")]
    public class SetActiveMissionCommand : Command
    {
        [SerializeField] private Mission mission = default;
        [SerializeField] private bool isActive = true;
        
        override public void OnEnter()
        {
            mission.SetAsActive(isActive);
            Continue();
        }
    }
}