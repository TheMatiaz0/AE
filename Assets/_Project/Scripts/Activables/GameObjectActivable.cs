using System;
using System.Collections.Generic;
using UnityEngine;

namespace AE
{
    [Serializable]
    public class GameObjectActivable : IActivable
    {
        [SerializeField]
        private List<GameObject> objectsToActivate;

        [SerializeField]
        private List<GameObject> objectsToDeactivate;

        public void Activate(IContext context)
        {
            foreach (var obj in objectsToActivate)
            {
                if (obj != null && obj)
                {
                    obj.SetActive(true);
                }
            }
            foreach (var obj in objectsToDeactivate)
            {
                if (obj != null && obj)
                {
                    obj.SetActive(false);
                }
            }
        }

        public void Deactivate(IContext context)
        {
            foreach (var obj in objectsToActivate)
            {
                if (obj != null && obj)
                {
                    obj.SetActive(false);
                }
            }
            foreach (var obj in objectsToDeactivate)
            {
                if (obj != null && obj)
                {
                    obj.SetActive(true);
                }
            }
        }
    }
}
