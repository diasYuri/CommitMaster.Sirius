using System;

namespace CommitMaster.Sirius.App.Responses.v1.AlunoUseCases
{
    public class AssinarPlanoCommandResponse
    {
        public Guid PagamentoId { get; set; }
        public string Message { get; set; }
    }
}