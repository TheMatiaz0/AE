namespace AE
{
    public class OnEnableActivator : BaseActivator
    {
        private void Awake()
        {
            base.Deactivate();
        }

        private void OnEnable()
        {
            base.Activate();
        }

        private void OnDisable()
        {
            base.Deactivate();
        }
    }
}
