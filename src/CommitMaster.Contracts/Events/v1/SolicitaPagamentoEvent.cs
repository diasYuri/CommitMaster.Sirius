using System;
using CommitMaster.Contracts.Mensageria;

namespace CommitMaster.Contracts.Events.v1
{
    public class SolicitaPagamentoEvent : Message, ISolicitacaoPagamento
    {
        public Guid Id { get; set; }
        public Guid SubscriptionId { get; set; }
        public int PaymentType { get; set; }
        public decimal Amount { get; set; }
        public int Installments { get; set; }
        public ISolicitacaoPagamento.PaymentCard PaymentCardInfo { get; set; }
        public ISolicitacaoPagamento.PaymentPayer PaymentPayerInfo { get; set; }
    }
}
