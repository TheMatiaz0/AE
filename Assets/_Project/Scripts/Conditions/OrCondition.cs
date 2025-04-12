using System;
using System.Collections.Generic;
using UnityEngine;

namespace AE
{
    [Serializable]
    public class OrCondition : ICondition
    {
        [SerializeReference, SubclassSelector] private List<ICondition> conditions;

        public bool IsConditionMet(IContext context)
        {
            return conditions.Exists(c => c.IsConditionMet(context));
        }
    }
}
