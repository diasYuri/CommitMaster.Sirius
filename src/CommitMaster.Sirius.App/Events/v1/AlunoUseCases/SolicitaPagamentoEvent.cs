using System;
using CommitMaster.Sirius.Domain.Contracts.v1.Mensageria;
using MediatR;

namespace CommitMaster.Sirius.App.Events.v1.AlunoUseCases
{
    public class SolicitaPagamentoEvent : INotification, ISolicitacaoPagamento
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
