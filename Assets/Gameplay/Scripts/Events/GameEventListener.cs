using System;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Events
{
    [Serializable]
    public class CustomGameEvent : UnityEvent<Component, object> { }
    
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] private GameEvent gameEvent = default;
        [SerializeField] private CustomGameEvent onEventExecuted = default;

        private void OnEnable() => gameEvent.RegisterListener(this);
        private void OnDisable() => gameEvent.UnregisterListener(this);

        public void OnEventRaised(Component sender, object data) => onEventExecuted?.Invoke(sender, data);
    }
}