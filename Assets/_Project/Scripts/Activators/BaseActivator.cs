using System.Collections.Generic;
using UnityEngine;

namespace AE
{
    public class BaseActivator : MonoBehaviour
    {
        [SerializeField, Tooltip("Looks for ANY condition instead of ALL")] private bool lookForAnyCondition;
        [SerializeReference, SubclassSelector] private List<ICondition> conditions;
        [SerializeReference, SubclassSelector] private IActivable result;

        public virtual void Activate(IContext context = null)
        {
            bool conditionsMet = lookForAnyCondition
                ? conditions.Exists(c => c.IsConditionMet(context))
                : conditions.TrueForAll(c => c.IsConditionMet(context));

            if (!conditionsMet) return;

            result.Activate();
        }

        public virtual void Deactivate(IContext context = null)
        {
            bool conditionsMet = lookForAnyCondition
                ? conditions.Exists(c => c.IsConditionMet(context))
                : conditions.TrueForAll(c => c.IsConditionMet(context));

            if (!conditionsMet) return;

            result.Deactivate();
        }
    }
}
