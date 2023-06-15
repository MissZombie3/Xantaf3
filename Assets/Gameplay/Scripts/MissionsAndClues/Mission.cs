using System;
using UnityEngine;

namespace Gameplay.MissionsAndClues
{
    [Serializable]
    public class Mission
    {
        [SerializeField] private string key = default;
        [SerializeField, TextArea] private string description = default;

        public string Key => key;
        public string Description => description;
        public bool IsActive { get; private set; }
        public bool IsCompleted { get; private set; }

        public void Complete()
        {
            if(IsActive)
                IsCompleted = true;
        }

        public void SetActive() => IsActive = true;
    }
}