using System;
using UnityEngine;

namespace Gameplay.MissionsAndClues
{
    [Serializable]
    public class Clue
    {
        [SerializeField] private string key = default;
        [SerializeField] private string name = default;

        public string Key => key;
        public string Name => name;
        public bool IsFound { get; private set; }

        public void Found() => IsFound = true;
    }
}