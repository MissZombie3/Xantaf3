using DG.Tweening;
using Gameplay.MissionsAndClues;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIClueVisualizer : MonoBehaviour
{
    [SerializeField] private Image clueSprite = default;
    [SerializeField] private TextMeshProUGUI clueName = default;
    [SerializeField] private TextMeshProUGUI clueDesc = default;

    private CanvasGroup canvasGroup = default;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void Show(Clue clue)
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        clueSprite.sprite = clue.Sprite;
        clueName.text = clue.Name;
        clueDesc.text = clue.Description;
        
        canvasGroup.DOKill();
        canvasGroup.DOFade(1, 0.5F);
    }

    public void Hide()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.DOKill();
        canvasGroup.DOFade(0, 0.25F);
    }
}