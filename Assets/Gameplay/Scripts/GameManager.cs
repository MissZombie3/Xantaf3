using System;
using System.Collections.Generic;
using Gameplay.Events;
using Gameplay.MissionsAndClues;
using UnityEngine;

namespace Gameplay
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private List<Mission> missions = new();
        [SerializeField] private List<Clue> clues = new();
        [SerializeField] private GameEvent missionsUpdated = default;
        [SerializeField] private GameEvent cluesUpdated = default;

        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            if (Instance != this)
                Destroy(gameObject);
        }

        private void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }

        public void CompleteMission(string key)
        {
            if (!TryGetMission(key, out Mission mission))
                throw new Exception($"Mission with key {key} not found.");
            mission.Complete();
            missionsUpdated.RaiseEvent(this, mission);
        }

        public void FoundClue(string key)
        {
            if (!TryGetClue(key, out Clue clue))
                throw new Exception($"Clue with key {key} not found.");
            clue.Found();
            cluesUpdated.RaiseEvent(this, clue);
        }

        public bool TryGetMission(string key, out Mission mission)
        {
            mission = missions.Find(m => m.Key == key);
            return mission != null;
        }
        
        public bool TryGetClue(string key, out Clue clue)
        {
            clue = clues.Find(c => c.Key == key);
            return clue != null;
        }
    }
}
