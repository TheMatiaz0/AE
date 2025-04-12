using System;
using System.Collections.Generic;
using UnityEngine;

namespace AE
{
    [Serializable]
    public class MultiActivatorActivable : IActivable
    {
        [SerializeReference, SubclassSelector] private List<IActivable> activables;

        public void Activate()
        {
            foreach (var activable in activables)
            {
                activable.Activate();
            }
        }

        public void Deactivate()
        {
            foreach (var activable in activables)
            {
                activable.Deactivate();
            }
        }
    }
}
