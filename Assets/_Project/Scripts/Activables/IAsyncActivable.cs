using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AE
{
    public interface IAsyncActivable : IActivable
    {
        UniTask ActivateAsync(IContext context);
        UniTask DeactivateAsync(IContext context);
    }
}
