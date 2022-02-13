using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommitMaster.Contracts.Events.v1;
using CommitMaster.Sirius.App.Commands.v1.AlunoUseCases;
using CommitMaster.Sirius.App.Responses.v1;
using CommitMaster.Sirius.App.Responses.v1.AlunoUseCases;
using CommitMaster.Sirius.Infra.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommitMaster.Sirius.App.Handlers.CommandHandlers.v1.AlunoUseCases
{
    public class AtivarAssinaturaCommandHandler : 
        HandlerResponseBase,
        IRequestHandler<AtivarAssinaturaCommand, HandlerResponse<AtivarAssinaturaCommandResponse>>
    {
        private readonly SiriusAppContext _appContext;
        private readonly IMediator _mediator;

        public AtivarAssinaturaCommandHandler(SiriusAppContext appContext, IMediator mediator)
        {
            _appContext = appContext;
            _mediator = mediator;
        }

        public async Task<HandlerResponse<AtivarAssinaturaCommandResponse>> Handle(AtivarAssinaturaCommand request, CancellationToken cancellationToken)
        {
            var assinatura = await _appContext
                .Assinaturas
                .Where(a => a.Id == request.AssinaturaId)
                .FirstOrDefaultAsync(cancellationToken);

            if (assinatura == null) {
                return ErroCommand<AtivarAssinaturaCommandResponse>("Não encontrado", "Assinatura não encontrada");
            }
            
            if (request.PagamentoComSucesso) {
                assinatura.Ativar();
            }
            else {
                assinatura.PagamentoRejeitado();
            }

            _appContext.Assinaturas.Update(assinatura);
            var success = await _appContext.SaveChangesAsync(cancellationToken) > 1;

            if (success) {
                await SendAlunoAtivoEvent(assinatura.AlunoId, cancellationToken);
            }
            
            return Sucesso(new AtivarAssinaturaCommandResponse {
                Sucesso = success
            });
        }


        private async Task SendAlunoAtivoEvent(Guid alunoId, CancellationToken cancellationToken)
        {
            var aluno = await _appContext.Alunos
                .Where(a => a.Id == alunoId)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            if (aluno != null) {
                await _mediator.Publish(new AlunoAtivoEvent {
                    Ativo = true,
                    Email = aluno.Email
                }, cancellationToken);
            }
        }
    }
}
