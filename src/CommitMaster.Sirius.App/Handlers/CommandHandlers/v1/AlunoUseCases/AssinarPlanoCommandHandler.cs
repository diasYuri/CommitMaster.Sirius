using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommitMaster.Sirius.App.Commands.v1.AlunoUseCases;
using CommitMaster.Sirius.App.Events.v1.AlunoUseCases;
using CommitMaster.Sirius.App.Responses.v1;
using CommitMaster.Sirius.App.Responses.v1.AlunoUseCases;
using CommitMaster.Sirius.Domain.Contracts.v1.Mensageria;
using CommitMaster.Sirius.Domain.Entities;
using CommitMaster.Sirius.Infra.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommitMaster.Sirius.App.Handlers.CommandHandlers.v1.AlunoUseCases
{
    public class CriarAssinaturaCommandHandler :
        HandlerResponseBase,
        IRequestHandler<AssinarPlanoCommand, HandlerResponse<AssinarPlanoCommandResponse>>
    {
        private readonly SiriusAppContext _appContext;
        private readonly IMediator _mediator;


        public CriarAssinaturaCommandHandler(SiriusAppContext appContext, IMediator mediator)
        {
            _appContext = appContext;
            _mediator = mediator;
        }

        public async Task<HandlerResponse<AssinarPlanoCommandResponse>> Handle(AssinarPlanoCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid(out var errors)) {
                return FailValidation<AssinarPlanoCommandResponse>(errors);
            }
            
            var aluno = await _appContext
                .Alunos
                .Where(p => p.Id == request.AlunoId)
                .Include(a => a.Assinatura)
                .FirstOrDefaultAsync(cancellationToken);
            
            if (aluno == null) {
                return ErroCommand<AssinarPlanoCommandResponse>("Erro", "Esse Plano não existe");
            }

            var plano = await _appContext
                .Planos
                .Where(p => p.Id == request.PlanoId)
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync(cancellationToken);

            if (plano == null) {
                return ErroCommand<AssinarPlanoCommandResponse>("Erro", "Esse Plano não existe");
            }
            
            var sucessoAoAdicionaAssinatura = aluno.AdicionaAssinatura(new Assinatura(aluno, plano));
            if (!sucessoAoAdicionaAssinatura) {
                return VerificarAssinatura(aluno);
            }
            
            _appContext.Alunos.Update(aluno);
            _appContext.Assinaturas.Add(aluno.Assinatura);
            
            var sucesso = await _appContext.SaveChangesAsync(cancellationToken) > 0;
            if (!sucesso) {
                return ErroInterno<AssinarPlanoCommandResponse>();
            }


            var pagamentoEvent = new SolicitaPagamentoEvent {
                Id = Guid.NewGuid(),
                SubscriptionId = aluno.Assinatura.Id,
                Amount = plano.Preco,
                Installments = request.DadoPagamento.NumeroParcela,
                PaymentType = request.DadoPagamento.TipoPagamento,
                PaymentCardInfo = new ISolicitacaoPagamento.PaymentCard {
                    Email = aluno.Email,
                    Name = request.DadoPagamento.Nome,
                    NumberDocument = request.DadoPagamento.Cpf,
                },
                PaymentPayerInfo = new ISolicitacaoPagamento.PaymentPayer {
                    CardNumber = request.DadoPagamento.NumeroCartao,
                    CVV = request.DadoPagamento.CVV,
                    ExpiryDate = request.DadoPagamento.DataVencimento
                }
            };

            await _mediator.Publish(pagamentoEvent, cancellationToken);

            return SucessoCriado(
                new AssinarPlanoCommandResponse {
                    PagamentoId = aluno.Assinatura.Id, 
                    Message = "Pagamento em processamento"
                });
        }

        private HandlerResponse<AssinarPlanoCommandResponse> VerificarAssinatura(Aluno aluno)
        {
            var (temResultado, message) = aluno.VerificarAssinatura();
            if (temResultado) {
                return ErroCommand<AssinarPlanoCommandResponse>("Aluno já possui uma assinatura", $"Estado da assinatura atual: {message}");
            }

            return ErroInterno<AssinarPlanoCommandResponse>();
        }
    }
}
