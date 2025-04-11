using System;
using UnityEngine;

namespace AE
{
    [Serializable]
    public class SpecificItemCondition : ICondition
    {
        [SerializeField] private Item requiredItem;

        public bool IsConditionMet(InteractionController controller)
        {
            return controller.HeldItem == requiredItem;
        }
    }
}
