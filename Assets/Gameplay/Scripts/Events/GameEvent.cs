using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Events
{
    [CreateAssetMenu(menuName = "Gameplay/Events/GameEvent", fileName = "GameEvent")]
    public class GameEvent : ScriptableObject
    {
        private readonly HashSet<GameEventListener> listeners = new();

        public void RegisterListener(GameEventListener listener) => listeners.Add(listener);
        public void UnregisterListener(GameEventListener listener) => listeners.Remove(listener);
        
        public void RaiseEvent(Component sender, object data)
        {
            foreach (GameEventListener l in listeners)
            {
                if (l == null)
                {
                    Debug.LogWarning($"Null listener on event {name}");
                    continue;
                }
                l.OnEventRaised(sender, data);
            }
        }
    }
}