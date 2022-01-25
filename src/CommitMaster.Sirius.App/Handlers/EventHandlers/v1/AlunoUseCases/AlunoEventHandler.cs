using System.Threading;
using System.Threading.Tasks;
using CommitMaster.Sirius.App.Events.v1.AlunoUseCases;
using MediatR;

namespace CommitMaster.Sirius.App.Handlers.EventHandlers.v1.AlunoUseCases
{
    public class AlunoEventHandler : 
        INotificationHandler<AlunoCriadoEvent>,
        INotificationHandler<SolicitaPagamentoEvent>,
        INotificationHandler<AlunoAtivoEvent>
    {

        public Task Handle(AlunoCriadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(SolicitaPagamentoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(AlunoAtivoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
