using UnityEngine;

namespace AE
{
    public class TriggerActivator : BaseActivator
    {
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
            if (other.TryGetComponent<IPlayerContext>(out var context) && !wasDeactivated)
            {
                base.Deactivate(context);
                wasDeactivated = true;
            }
        }
    }
}
