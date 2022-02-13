using MediatR;

namespace CommitMaster.Contracts.Mensageria
{
    public interface IMessage
    {
        
    }

    public abstract class Message : IMessage, INotification
    {
        
    }
}
