using System;
using Gameplay.MissionsAndClues;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIClueButton : MonoBehaviour
{
    [SerializeField] private Clue clue = default;
    [SerializeField] private GameObject unlockedContainer = default;
    [SerializeField] private Image clueSprite = default;
    [SerializeField] private TextMeshProUGUI clueName = default;
    [SerializeField] private GameObject newContainer = default;

    private Button myButton = default;
    private UIClueVisualizer visualizer = default;

    public bool IsCompleted => clue == null || clue.IsFound;
    public bool IsViewed { get; private set; }

    private void Awake()
    {
        if (clue == null)
        {
            gameObject.SetActive(false);
            IsViewed = true;
            return;
        }
        visualizer = FindObjectOfType<UIClueVisualizer>();
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(OpenVisualization);
        clue.Updated += OnClueUpdated;
    }

    private void OnDestroy()
    {
        if (clue == null)
            return;
        myButton.onClick.RemoveListener(OpenVisualization);
        clue.Updated -= OnClueUpdated;
    }

    private void Update()
    {
        newContainer.SetActive(IsCompleted && !IsViewed);
    }

    private void OpenVisualization()
    {
        if (clue.IsFound)
        {
            IsViewed = true;
            visualizer.Show(clue);
        }
    }

    private void OnClueUpdated()
    {
        clueSprite.sprite = clue.Sprite;
        clueName.text = clue.Name;
        unlockedContainer.SetActive(clue.IsFound);
    }
}