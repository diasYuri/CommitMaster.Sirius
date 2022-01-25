using System.Threading.Tasks;
using CommitMaster.Sirius.Domain.Contracts.v1.Mensageria;
using MassTransit;
using IBus = CommitMaster.Sirius.Domain.Contracts.v1.Bus.IBus;

namespace CommitMaster.Sirius.Infra.MessageBroker
{
    public class Bus : IBus
    {
        private readonly IBusControl _busControl;

        public Bus(IBusControl busControl)
        {
            _busControl = busControl;
        }
        
        public async Task Publish<T>(T data) where T : class, IMessage
        {
            await _busControl.Publish(data);
        }
    }
}
