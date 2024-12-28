using PaymentContext.Shared.Commands;

namespace PaymentContext.Shared.Commands
{
    public interface IHandler<T> where T : ICommand
    {
        ICommandResult Handle(T command);
    }
}