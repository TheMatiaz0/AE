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

        public void Activate()
        {
            foreach (var obj in objectsToActivate)
            {
                obj.SetActive(true);
            }
            foreach (var obj in objectsToDeactivate)
            {
                obj.SetActive(false);
            }
        }

        public void Deactivate()
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
