using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Events
{
    public delegate void GameEventHandler(Component component, object data);
    
    [CreateAssetMenu(menuName = "Gameplay/Events/GameEvent", fileName = "GameEvent")]
    public class GameEvent : ScriptableObject
    {
        private readonly HashSet<GameEventHandler> listeners = new();

        public void RegisterListener(GameEventHandler listener) => listeners.Add(listener);
        public void UnregisterListener(GameEventHandler listener) => listeners.Remove(listener);
        
        public void RaiseEvent(Component sender, object data)
        {
            foreach (GameEventHandler l in listeners)
            {
                if (l == null)
                {
                    Debug.LogWarning($"Null listener on event {name}");
                    continue;
                }
                l.Invoke(sender, data);
            }
        }
    }
}