using System.Linq;
using Gameplay.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIOpenCluesButton : MonoBehaviour
{
    [SerializeField] private GameObject badge = default;
    [SerializeField] private TextMeshProUGUI badgeCount = default;
    
    private PlayerMovement playerMovement = default;
    private Button myButton = default;
    private UIClueButton[] allClues = default;

    private void Awake()
    {
        myButton = GetComponent<Button>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        allClues = FindObjectsOfType<UIClueButton>();
    }

    private void Update()
    {
        myButton.interactable = !playerMovement.IsDisabledByFungus;
        int count = allClues.Count(c => c.IsCompleted && !c.IsViewed);
        if (count > 0)
            badgeCount.text = count.ToString();
        badge.SetActive(count > 0);
    }
}