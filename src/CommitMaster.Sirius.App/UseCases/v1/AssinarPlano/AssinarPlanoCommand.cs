using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CommitMaster.Sirius.Domain.Contracts;
using MediatR;

namespace CommitMaster.Sirius.App.UseCases.v1.AssinarPlano
{
    public class AssinarPlanoCommand : IRequest<HandlerResponse<AssinarPlanoCommandResponse>> , ICommandValidable
    {
        [JsonIgnore]
        public Guid AlunoId { get; set; }
        
        [Required(ErrorMessage = "O Campo AssinaturaId é obrigatório")]
        public Guid PlanoId { get; set; }

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
