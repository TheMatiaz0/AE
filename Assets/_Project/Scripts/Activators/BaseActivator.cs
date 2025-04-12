using UnityEngine;

namespace AE
{
    public class BaseActivator : MonoBehaviour
    {
        [SerializeReference, SubclassSelector] private ICondition conditions;
        [SerializeReference, SubclassSelector] private IActivable result;

        public virtual void Activate(IContext context = null)
        {
            if (conditions != null && !conditions.IsConditionMet(context))
            {
                return;
            }

            result.Activate(context);
        }

        public virtual void Deactivate(IContext context = null)
        {
            if (conditions != null && !conditions.IsConditionMet(context))
            {
                return;
            }

            result.Deactivate(context);
        }
    }
}
