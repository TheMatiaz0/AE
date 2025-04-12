using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AE
{
    [Serializable]
    public class MultiActivatorActivable : IActivable
    {
        [SerializeReference, SubclassSelector] private List<IActivable> activables;

        protected List<IActivable> Activables => activables;

        public void Activate(IContext context)
        {
            foreach (var activable in activables)
            {
                activable.Activate(context);
            }
        }

        public void Deactivate(IContext context)
        {
            foreach (var activable in activables)
            {
                activable.Deactivate(context);
            }
        }
    }
}
