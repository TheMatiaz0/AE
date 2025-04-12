namespace AE
{
    public interface IActivable
    {
        void Activate(IContext context);
        void Deactivate(IContext context);
    }
}
