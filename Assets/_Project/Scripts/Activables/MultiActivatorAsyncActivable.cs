using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AE
{
    [Serializable]
    public class MultiActivatorAsyncActivable : IAsyncActivable
    {
        [SerializeReference, SubclassSelector] private List<IActivable> activables;

        public void Activate(IContext context)
        {
            ActivateAsync(context).Forget();
        }

        public void Deactivate(IContext context)
        {
            DeactivateAsync(context).Forget();
        }

        public async UniTask ActivateAsync(IContext context)
        {
            foreach (var activable in activables)
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
            foreach (var activable in activables)
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
