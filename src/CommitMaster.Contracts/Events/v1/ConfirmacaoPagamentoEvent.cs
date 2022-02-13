using System;
using CommitMaster.Contracts.Mensageria;

namespace CommitMaster.Contracts.Events.v1
{
    public class ConfirmacaoPagamentoEvent : Message
    {
        public Guid AssinaturaId { get; set; }
        public bool PagamentoComSucesso { get; set; }
        public DateTime DataPagamento { get; set; }
    }
}
