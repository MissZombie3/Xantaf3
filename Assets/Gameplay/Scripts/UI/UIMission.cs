using DG.Tweening;
using Gameplay.MissionsAndClues;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class UIMission : MonoBehaviour
    {
        [SerializeField] private Mission mission = default;
        [SerializeField] private TextMeshProUGUI missionText = default;
        [SerializeField] private Image completedIndicator = default;
        [Header("Transition")]
        [SerializeField] private float showDuration = 0.25F;
        [SerializeField] private Ease showEase = Ease.OutBack;
        [SerializeField] private float hideDuration = 0.25F;
        [SerializeField] private Ease hideEase = Ease.InBack;

        private FontStyles originalStyle = default;
        private bool isVisible;

        private void Awake()
        {
            missionText.text = mission.Description;
            originalStyle = missionText.fontStyle;
            mission.Updated += OnMissionOnUpdated;
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            mission.Updated -= OnMissionOnUpdated;
        }

        private void OnMissionOnUpdated()
        {
            bool wasVisible = isVisible;
            isVisible = mission.IsActive;

            if (!wasVisible && isVisible)
                Show();

            if (wasVisible && !isVisible)
                Hide();
            
            if (!isVisible) return;
            completedIndicator.enabled = mission.IsCompleted;
            missionText.fontStyle = mission.IsCompleted ? FontStyles.Strikethrough : originalStyle;
        }

        private void Show()
        {
            gameObject.SetActive(true);
            transform.DOKill();
            transform.localScale = new Vector3(0, 1, 1);
            transform.DOScale(Vector3.one, showDuration).SetEase(showEase);
        }

        private void Hide()
        {
            transform.DOKill();
            transform.localScale = new Vector3(1, 1, 1);
            transform.DOScale(new Vector3(0, 1, 1), hideDuration).SetEase(hideEase).OnComplete(() => gameObject.SetActive(false));
        }
    }
}