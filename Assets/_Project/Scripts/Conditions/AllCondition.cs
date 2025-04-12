using System;
using System.Collections.Generic;
using UnityEngine;

namespace AE
{
    [Serializable]
    public class AllCondition : ICondition
    {
        [SerializeReference, SubclassSelector] private List<ICondition> conditions;

        public bool IsConditionMet(IContext context)
        {
            return conditions.TrueForAll(c => c != null && c.IsConditionMet(context));
        }
    }
}
