using UnityEngine;

namespace AE
{
    public class TriggerActivator : BaseActivator
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IPlayerContext>(out var context))
            {
                base.Activate(context);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<IPlayerContext>(out var context))
            {
                base.Deactivate(context);
            }
        }
    }
}
