using System;
using Gameplay.Events;
using UnityEngine;

namespace Gameplay.MissionsAndClues
{
    [CreateAssetMenu(menuName="Xantaf3/Clue")]
    public class Clue : ScriptableObject
    {
        public event Action Updated;
        
        [SerializeField] private string clueName = default;
        [SerializeField] private string clueDescription = default;

        public string Name => clueName;
        public string Description => clueDescription;
        public bool IsFound { get; private set; }

        private void OnEnable()
        {
            IsFound = false;
        }

        public void Found()
        {
            if (IsFound)
                return;
            IsFound = true;
            Updated?.Invoke();
        }
    }
}