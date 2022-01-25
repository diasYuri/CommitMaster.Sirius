using System;

namespace CommitMaster.Sirius.Domain.Contracts.v1.Mensageria
{
    public interface ISolicitacaoPagamento : IMessage
    {
        public Guid Id { get; set; }
        public Guid SubscriptionId { get; set; }
        public int PaymentType { get; set; }
        public decimal Amount { get; set; }
        public int Installments { get; set; }
        public PaymentCard PaymentCardInfo { get; set; }
        public PaymentPayer PaymentPayerInfo { get; set; }
        

        public class PaymentPayer
        {
            public string CardNumber { get; set; }
            public string CVV { get; set; }
            public string ExpiryDate { get; set; }
        }
        public class PaymentCard
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string NumberDocument { get; set; }
        }
        
    }
}
