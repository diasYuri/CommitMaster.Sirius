using System;

namespace CommitMaster.Sirius.App.UseCases.v1.AssinarPlano
{
    public class AssinarPlanoCommandResponse
    {
        public Guid PagamentoId { get; set; }
        public string Message { get; set; }
    }
}