using UnityEngine;

namespace AE
{
    public interface ICondition
    {
        bool IsConditionMet(InteractionController controller);
    }
}
