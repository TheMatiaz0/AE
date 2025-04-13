using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;

namespace AE
{
    [Serializable]
    public class WaitActivable : IAsyncActivable
    {
        [SerializeField] private int milisecondsDelay;

        public void Activate(IContext context)
        {
            _ = ActivateAsync(context);
        }

        public void Deactivate(IContext context)
        {
        }

        public async UniTask ActivateAsync(IContext context)
        {
            await UniTask.Delay(milisecondsDelay);
        }

        public UniTask DeactivateAsync(IContext context)
        {
            Deactivate(context);
            return UniTask.CompletedTask;
        }
    }
}
