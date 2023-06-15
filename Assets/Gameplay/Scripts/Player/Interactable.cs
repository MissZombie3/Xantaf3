using Fungus;
using Gameplay.QuickOutline.Scripts;
using UnityEngine;

namespace Gameplay.Player
{
    [RequireComponent(typeof(Collider), typeof(Outline), typeof(Flowchart))]
    public class Interactable : MonoBehaviour
    {
        private Outline outline = default;
        private Flowchart flowchart = default;

        private void Awake()
        {
            outline = GetComponent<Outline>();
            flowchart = GetComponent<Flowchart>();
            SetHighlight(false);
        }

        private void OnValidate()
        {
            if (gameObject.layer != LayerMask.NameToLayer("Interactable"))
                gameObject.layer = LayerMask.NameToLayer("Interactable");
        }

        public void SetHighlight(bool isFocused) => outline.enabled = isFocused;

        public void Interact()
        {
            if (!flowchart.ExecuteIfHasBlock("Interact"))
                Debug.LogWarning("El flowchart no tiene el bloque Interact.");
        }
    }
}