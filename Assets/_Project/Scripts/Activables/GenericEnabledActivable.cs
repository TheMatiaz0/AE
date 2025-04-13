using System.Collections.Generic;
using UnityEngine;

namespace AE
{
    public abstract class GenericEnabledActivable<T> : IActivable where T : Object
    {
        [SerializeField]
        private List<T> objectsToActivate;

        [SerializeField]
        private List<T> objectsToDeactivate;

        protected abstract void SetActive(T t, bool isActive);

        public void Activate(IContext context)
        {
            foreach (var obj in objectsToActivate)
            {
                if (obj != null && obj)
                {
                    SetActive(obj, true);
                }
            }
            foreach (var obj in objectsToDeactivate)
            {
                if (obj != null && obj)
                {
                    SetActive(obj, false);
                }
            }
        }

        public void Deactivate(IContext context)
        {
            foreach (var obj in objectsToActivate)
            {
                if (obj != null && obj)
                {
                    SetActive(obj,false);
                }
            }
            foreach (var obj in objectsToDeactivate)
            {
                if (obj != null && obj)
                {
                    SetActive(obj,true);
                }
            }
        }
    }
}
