using DG.Tweening;
using Gameplay.Events;
using TMPro;
using UnityEngine;

namespace Gameplay.UI
{
    public class UIMessage : MonoBehaviour
    {
        [Header("Log")]
        [SerializeField] private GameEvent logEvent = default;
        [SerializeField] private CanvasGroup logContainer = default;
        [SerializeField] private TextMeshProUGUI logText = default;
        [Header("Warning")]
        [SerializeField] private GameEvent warningEvent = default;
        [SerializeField] private CanvasGroup warningContainer = default;
        [SerializeField] private TextMeshProUGUI warningText = default;
        [Header("Error")]
        [SerializeField] private GameEvent errorEvent = default;
        [SerializeField] private CanvasGroup errorContainer = default;
        [SerializeField] private TextMeshProUGUI errorText = default;
        [Header("Error")]
        [SerializeField] private float showDuration = 0.25F;
        [SerializeField] private float inScreenDuration = 3.0F;
        [SerializeField] private float hideDuration = 0.25F;

        private void Awake()
        {
            logContainer.alpha = 0;
            warningContainer.alpha = 0;
            errorContainer.alpha = 0;
        }

        private void OnEnable()
        {
            logEvent.RegisterListener(OnLogExecuted);
            warningEvent.RegisterListener(OnWarningExecuted);
            errorEvent.RegisterListener(OnErrorExecuted);
        }

        private void OnDisable()
        {
            logEvent.UnregisterListener(OnLogExecuted);
            warningEvent.UnregisterListener(OnWarningExecuted);
            errorEvent.UnregisterListener(OnErrorExecuted);
        }

        private void OnLogExecuted(Component component, object data)
        {
            string message = (string)data;
            logContainer.alpha = 0;
            logText.text = message;
            logContainer.DOKill();
            logContainer.DOFade(1, showDuration).OnComplete(() =>
            {
                logContainer.DOFade(0, hideDuration).SetDelay(inScreenDuration);
            });
        }
        
        private void OnWarningExecuted(Component component, object data)
        {
            string message = (string)data;
            warningContainer.alpha = 0;
            warningText.text = message;
            warningContainer.DOKill();
            warningContainer.DOFade(1, showDuration).OnComplete(() =>
            {
                warningContainer.DOFade(0, hideDuration).SetDelay(inScreenDuration);
            });
        }
        
        private void OnErrorExecuted(Component component, object data)
        {
            string message = (string)data;
            errorContainer.alpha = 0;
            errorText.text = message;
            errorContainer.DOKill();
            errorContainer.DOFade(1, showDuration).OnComplete(() =>
            {
                errorContainer.DOFade(0, hideDuration).SetDelay(inScreenDuration);
            });
        }
    }
}