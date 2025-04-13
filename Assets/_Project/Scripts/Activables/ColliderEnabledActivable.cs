using System;
using UnityEngine;

namespace AE
{
    [Serializable]
    public class ColliderEnabledActivable : GenericEnabledActivable<Collider>
    {
        protected override void SetActive(Collider t, bool isActive)
        {
            t.enabled = isActive;
        }
    }
}
