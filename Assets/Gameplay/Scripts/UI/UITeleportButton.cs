using System;
using Fungus;
using Gameplay.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
    [RequireComponent(typeof(Button))]
    public class UITeleportButton : MonoBehaviour
    {
        [SerializeField] private float duration = 1;
        [SerializeField] private Transform destination = default;
        
        private Button myButton = default;
        private PlayerMovement playerMovement = default;

        private void Awake()
        {
            myButton = GetComponent<Button>();
            myButton.onClick.AddListener(OnClick);
            playerMovement = FindObjectOfType<PlayerMovement>();
        }

        private void OnClick()
        {
            if(playerMovement.IsTeleporting)
                return;
            playerMovement.StartTeleport(duration / 2F);
            FungusManager.Instance.CameraManager.Fade(1, duration / 2F, () => playerMovement.transform.position = destination.position);
            FungusManager.Instance.CameraManager.Fade(0, duration / 2F, null);
        }
    }
}