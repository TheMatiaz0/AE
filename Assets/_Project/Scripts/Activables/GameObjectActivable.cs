using System.Collections.Generic;
using UnityEngine;

namespace AE
{
    public class GameObjectActivable : MonoBehaviour, IActivable
    {
        [SerializeField]
        private List<GameObject> objectsToActivate;

        [SerializeField]
        private List<GameObject> objectsToDeactivate;

        private void Awake()
        {
            Deactivate();
        }

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
