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
                else
                {
                    Debug.LogWarning("Tried to activate a destroyed or missing object.");
                }
            }
            foreach (var obj in objectsToDeactivate)
            {
                if (obj != null && obj)
                {
                    obj.SetActive(false);
                }
                else
                {
                    Debug.LogWarning("Tried to deactivate a destroyed or missing object.");
                }
            }
        }

        public void Deactivate(IContext context)
        {
            foreach (var obj in objectsToActivate)
            {
                obj.SetActive(false);
            }
            foreach (var obj in objectsToDeactivate)
            {
                obj.SetActive(true);
            }
        }
    }
}
