using System;
using System.Threading.Tasks;
using CommitMaster.Contracts.Mensageria;

namespace CommitMaster.Sirius.Domain.Contracts.v1.Bus
{
    public interface IMessageBus
    {
        void Publish<T>(T data) where T : Message;
        Task PublishAsync<T>(T data) where T : Message;

        void SubscribeAsync<T>(string subscriptionId, Func<T, Task> onMessage) where T : Message;
    }
    
}
