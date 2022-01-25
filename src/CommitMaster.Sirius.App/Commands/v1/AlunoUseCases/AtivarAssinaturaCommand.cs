using System;
using CommitMaster.Sirius.App.Responses.v1;
using CommitMaster.Sirius.App.Responses.v1.AlunoUseCases;
using MediatR;

namespace CommitMaster.Sirius.App.Commands.v1.AlunoUseCases
{
    public class AtivarAssinaturaCommand : IRequest<HandlerResponse<AtivarAssinaturaCommandResponse>>
    {
        public Guid AssinaturaId { get; set; }
        public bool PagamentoComSucesso { get; set; }
    }

}
