using System.Threading;
using System.Threading.Tasks;
using CommitMaster.Contracts.Events.v1;
using CommitMaster.Sirius.Domain.Contracts.v1.Bus;
using MediatR;

namespace CommitMaster.Sirius.App.EventHandlers.v1.AlunoUseCases
{
    public class AlunoEventHandler :
        INotificationHandler<AlunoCriadoEvent>,
        INotificationHandler<SolicitaPagamentoEvent>,
        INotificationHandler<AlunoAtivoEvent>
    {
        private readonly IMessageBus _messageBus;

        public AlunoEventHandler(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public Task Handle(AlunoCriadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(SolicitaPagamentoEvent notification, CancellationToken cancellationToken)
        {
            _messageBus.Publish(notification);
            return Task.CompletedTask;
        }

        public Task Handle(AlunoAtivoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
