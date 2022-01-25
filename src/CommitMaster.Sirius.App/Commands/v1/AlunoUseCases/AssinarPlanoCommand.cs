using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CommitMaster.Sirius.App.Responses.v1;
using CommitMaster.Sirius.App.Responses.v1.AlunoUseCases;
using CommitMaster.Sirius.Domain.Contracts;
using MediatR;

namespace CommitMaster.Sirius.App.Commands.v1.AlunoUseCases
{
    public class AssinarPlanoCommand : IRequest<HandlerResponse<AssinarPlanoCommandResponse>> , ICommandValidable
    {
        [JsonIgnore]
        public Guid AlunoId { get; set; }
        
        [Required(ErrorMessage = "O Campo AssinaturaId é obrigatório")]
        public Guid PlanoId { get; set; }
        
        [Required(ErrorMessage = "O Campo Valor é obrigatório")]
        public decimal Valor { get; set; }

        [Required]
        public DadoPagamento DadoPagamento { get; set; }


        public bool IsValid(out List<ValidationResult> errors)
        {
            var ctx = new ValidationContext(this);
            errors = new List<ValidationResult>();

            return Validator.TryValidateObject(this, ctx, errors, true);
        }
    }

}
