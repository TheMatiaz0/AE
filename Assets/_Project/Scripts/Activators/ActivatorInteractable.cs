using UnityEngine;

namespace AE
{
    public class ActivatorInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private InteractablePrompt prompt;
        [SerializeField] private bool disableInteractableOnEnd = true;
        [SerializeReference, SubclassSelector] private ICondition conditions;
        [SerializeReference, SubclassSelector] private IActivable activateEffects;
        [SerializeReference, SubclassSelector] private IActivable deactivateEffects;

        public bool IsInteractable { get; set; } = true;
        public InteractablePrompt InteractionPrompt => prompt;

        public void Interact(IInteractionContext context)
        {
            if (conditions != null && !conditions.IsConditionMet(context))
            {
                return;
            }

            activateEffects?.Activate(context);
            deactivateEffects?.Deactivate(context);

            if (disableInteractableOnEnd)
            {
                IsInteractable = false;
            }
        }
    }
}
