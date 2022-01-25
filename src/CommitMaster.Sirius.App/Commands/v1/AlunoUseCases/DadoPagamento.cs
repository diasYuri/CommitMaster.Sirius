using System.ComponentModel.DataAnnotations;
using CommitMaster.Sirius.Domain.Utils;

namespace CommitMaster.Sirius.App.Commands.v1.AlunoUseCases
{
    public class DadoPagamento
    {
        [Required(ErrorMessage = "O Campo Nome é obrigatório")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "O Campo Cpf é obrigatório")]
        [CpfValidation.CustomValidationCPFAttribute(ErrorMessage = "O cpf é inválido")]
        [IsNumber(ErrorMessage = "O cpf deve conter apenas números")]
        public string Cpf { get; set; }
        
        [Required(ErrorMessage = "O Campo Numero Cartão é obrigatório")]
        [CreditCard(ErrorMessage = "O numero do cartão é inválido")]
        public string NumeroCartao { get; set; }
        
        [Required(ErrorMessage = "O Campo CVV é obrigatório")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "O campo cvv deve ter 3 digitos")]
        public string CVV { get; set; }
        
        [Required(ErrorMessage = "O Campo Data Vencimento é obrigatório")]
        public string DataVencimento { get; set; }

        [Required(ErrorMessage = "Tipo pagamento inválido")]
        public int TipoPagamento { get; set; }

        [Required(ErrorMessage = "Informe o número de parcelas")]
        public int NumeroParcela { get; set; }
    }
}