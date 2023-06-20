using Fungus;
using Gameplay.MissionsAndClues;
using UnityEngine;

namespace Gameplay.Commands
{
    [CommandInfo("Gameplay", "Completar Misión", "Completa una misión en específico.")]
    public class CompleteMissionCommand : Command
    {
        [SerializeField] private Mission mission = default;

        override public void OnEnter()
        {
            mission.Complete();
            Continue();
        }
    }
}