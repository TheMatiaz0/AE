using System.Collections.Generic;
using UnityEngine;

namespace AE
{
    public class BehaviourEnabledActivable : IActivable
    {
        [SerializeField] private List<Behaviour> componentsToEnable;
        [SerializeField] private List<Behaviour> componentsToDisable;

        public void Activate(IContext context)
        {
            foreach (var component in componentsToEnable)
            {
                if (component != null && component)
                {
                    component.enabled = true;
                }
            }
            foreach (var component in componentsToDisable)
            {
                if (component != null && component)
                {
                    component.enabled = false;
                }
            }
        }

        public void Deactivate(IContext context)
        {
            foreach (var component in componentsToEnable)
            {
                if (component != null && component)
                {
                    component.enabled = false;
                }
            }
            foreach (var component in componentsToDisable)
            {
                if (component != null && component)
                {
                    component.enabled = true;
                }
            }
        }
    }
}
