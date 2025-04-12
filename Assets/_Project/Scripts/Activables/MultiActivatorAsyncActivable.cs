using Cysharp.Threading.Tasks;

namespace AE
{
    public class MultiActivatorAsyncActivable : MultiActivatorActivable, IAsyncActivable
    {
        public async UniTask ActivateAsync(IContext context)
        {
            foreach (var activable in Activables)
            {
                if (activable is IAsyncActivable asyncActivable)
                {
                    await asyncActivable.ActivateAsync(context);
                }
                else
                {
                    activable.Activate(context);
                }
            }
        }

        public async UniTask DeactivateAsync(IContext context)
        {
            foreach (var activable in Activables)
            {
                if (activable is IAsyncActivable asyncActivable)
                {
                    await asyncActivable.DeactivateAsync(context);
                }
                else
                {
                    activable.Deactivate(context);
                }
            }
        }
    }
}
