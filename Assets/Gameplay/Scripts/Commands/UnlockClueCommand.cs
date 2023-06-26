using Fungus;
using Gameplay.Events;
using Gameplay.MissionsAndClues;
using UnityEngine;

namespace Gameplay.Commands
{
    [CommandInfo("Gameplay", "Desbloquear pista", "Desbloquea una pista en específico.")]
    public class UnlockClueCommand : Command
    {
        [SerializeField] private Clue clue = default;
        [SerializeField] private GameEvent logEvent;

        override public void OnEnter()
        {
            clue.Found();
            if (logEvent != null)
                logEvent.RaiseEvent(null, $"Encontraste {clue.Name}!");
            Continue();
        }
    }
}