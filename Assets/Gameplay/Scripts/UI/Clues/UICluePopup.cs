using System;
using System.Collections.Generic;
using DG.Tweening;
using Gameplay.MissionsAndClues;
using UnityEngine;

public class UICluePopup : MonoBehaviour
{
    [SerializeField] private List<Mission> allMissions = new List<Mission>();
    [SerializeField] private GameObject veredictButton = default;
    [SerializeField] private GameObject veredictButtonDisabled = default;
    [SerializeField] private GameObject messageButton = default;

    private CanvasGroup canvasGroup = default;

    public bool IsVisible { get; private set; }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void Show()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOKill();
        canvasGroup.DOFade(1, 1.5F);
        IsVisible = true;
    }

    public void Hide()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.DOKill();
        canvasGroup.DOFade(0, 0.5F);
        IsVisible = false;
    }

    private void Update()
    {
        veredictButton.SetActive(allMissions.TrueForAll(m => m.IsCompleted));
        messageButton.SetActive(!veredictButton.activeSelf);
        veredictButtonDisabled.SetActive(!veredictButton.activeSelf);
    }
}