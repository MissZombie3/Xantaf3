using System;
using Gameplay.Events;
using UnityEngine;

namespace Gameplay.MissionsAndClues
{
    [CreateAssetMenu(menuName="Xantaf3/Mission")]
    public class Mission : ScriptableObject
    {
        public event Action Updated;
        
        [SerializeField, TextArea] private string description = default;

        public string Description => description;
        public bool IsActive { get; private set; }
        public bool IsCompleted { get; private set; }

        private void OnEnable()
        {
            IsActive = false;
            IsCompleted = false;
            Updated?.Invoke();
        }

        public void SetAsActive(bool isActive)
        {
            if (isActive && IsCompleted)
            {
                Debug.LogError("Se está tratando de activar una misión que ya está completada.");
                return;
            }
            IsActive = isActive;
            Updated?.Invoke();
        }
        
        public void Complete()
        {
            if (IsActive)
            {
                IsCompleted = true;
                Updated?.Invoke();
            }
            else
                Debug.LogError("Se está tratando de completar una misión que no está activa.");
        }
    }
}