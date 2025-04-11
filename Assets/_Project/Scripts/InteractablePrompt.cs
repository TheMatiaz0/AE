using UnityEngine;

namespace AE
{
    [CreateAssetMenu(fileName = "InteractablePrompt", menuName = "Custom/Interactable/Prompt")]
    public class InteractablePrompt : ScriptableObject
    {
        [SerializeField]
        private Sprite indicator;

        public Sprite Indicator => indicator;
    }
}
