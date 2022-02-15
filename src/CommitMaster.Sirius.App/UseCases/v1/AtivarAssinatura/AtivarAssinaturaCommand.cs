using System;
using MediatR;

namespace CommitMaster.Sirius.App.UseCases.v1.AtivarAssinatura
{
    public class AtivarAssinaturaCommand : IRequest<HandlerResponse<AtivarAssinaturaCommandResponse>>
    {
        public Guid AssinaturaId { get; set; }
        public bool PagamentoComSucesso { get; set; }
    }

}
