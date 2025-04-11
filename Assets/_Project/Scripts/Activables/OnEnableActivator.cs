using UnityEngine;

namespace AE
{
    public class OnEnableActivator : MonoBehaviour
    {
        [SerializeReference, SubclassSelector] private IActivable activable; 

        private void Awake()
        {
            activable.Deactivate();
        }

        private void OnEnable()
        {
            activable.Activate();
        }

        private void OnDisable()
        {
            activable.Deactivate();
        }
    }
}
