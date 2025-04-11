using UnityEngine;

namespace AE
{
    public class TriggerActivator : MonoBehaviour
    {
        [SerializeReference, SubclassSelector] private IActivable activable;

        private void OnTriggerEnter(Collider other)
        {
            activable.Activate();
        }

        private void OnTriggerExit(Collider other)
        {
            activable.Deactivate();
        }
    }
}
