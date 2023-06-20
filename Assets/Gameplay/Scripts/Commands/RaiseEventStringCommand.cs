using Fungus;
using Gameplay.Events;
using UnityEngine;

namespace Gameplay.Commands
{
    [CommandInfo("Events", "Raise Event String", "Raise event with string data")]
    public class RaiseEventStringCommand : Command
    {
        [SerializeField] private GameEvent gameEvent = default;
        [SerializeField] private string data = default;

        override public void OnEnter()
        {
            gameEvent.RaiseEvent(this, data);
            Continue();
        }
    }
}