using System;
using UnityEngine;

namespace AE
{
    [Serializable]
    public class BehaviourEnabledActivable : GenericEnabledActivable<Behaviour>
    {
        protected override void SetActive(Behaviour t, bool isActive)
        {
            t.enabled = isActive;
        }
    }
}
