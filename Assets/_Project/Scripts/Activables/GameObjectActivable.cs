using System;
using UnityEngine;

namespace AE
{
    [Serializable]
    public class GameObjectActivable : GenericEnabledActivable<GameObject>
    {
        protected override void SetActive(GameObject t, bool isActive)
        {
            t.SetActive(isActive);
        }
    }
}
