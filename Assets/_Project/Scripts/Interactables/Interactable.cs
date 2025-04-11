using System.Collections.Generic;
using UnityEngine;

namespace AE
{
    public class Interactable : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private InteractablePrompt prompt;

        [SerializeReference, SubclassSelector] private List<ICondition> conditions;

        public InteractablePrompt GetInteractionPrompt()
        {
            return prompt;
        }

        public void Interact(InteractionController controller)
        {
            foreach (var condition in conditions)
            {
                if (condition.IsConditionMet(controller))
                {
                    // apply activator or sth
                }
            }
        }
    }
}
