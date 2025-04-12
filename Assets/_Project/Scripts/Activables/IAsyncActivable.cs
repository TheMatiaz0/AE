using Cysharp.Threading.Tasks;

namespace AE
{
    public interface IAsyncActivable : IActivable
    {
        UniTask ActivateAsync(IContext context);
        UniTask DeactivateAsync(IContext context);
    }
}
