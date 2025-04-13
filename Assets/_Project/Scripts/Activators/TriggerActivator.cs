using UnityEngine;

namespace AE
{
    public class TriggerActivator : BaseActivator
    {
        [SerializeField]
        private bool disableTriggerOnExit = false;

        private bool wasActivated;
        private bool wasDeactivated;    

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IPlayerContext>(out var context) && !wasActivated)
            {
                base.Activate(context);
                wasActivated = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (disableTriggerOnExit)
            {
                if (other.TryGetComponent<IPlayerContext>(out var context) && !wasDeactivated)
                {
                    base.Deactivate(context);
                    wasDeactivated = true;
                }
            }
        }
    }
}
