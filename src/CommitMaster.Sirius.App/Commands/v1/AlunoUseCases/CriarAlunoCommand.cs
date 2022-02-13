using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CommitMaster.Sirius.App.Responses.v1;
using CommitMaster.Sirius.App.Responses.v1.AlunoUseCases;
using CommitMaster.Sirius.Domain.Contracts;
using CommitMaster.Sirius.Domain.Utils;
using MediatR;

namespace CommitMaster.Sirius.App.Commands.v1.AlunoUseCases
{
    public class CriarAlunoCommand : IRequest<HandlerResponse<CriarAlunoCommandResponse>>, ICommandValidable
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [MaxLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "A Data de Aniversario é obrigatória")]
        public DateTime DataAniversario { get; set; }
        
        [Required(ErrorMessage = "O Campo Cpf é obrigatório")]
        [CpfValidation.CustomValidationCPFAttribute(ErrorMessage = "O cpf é inválido")]
        public string Cpf { get; set; }
        
        [Required(ErrorMessage = "O Telefone é obrigatório")]
        public string Telefone { get; set; }
        
        [Required(ErrorMessage = "O Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "A senha é obrigatória")]
        public string Senha { get; set; }




        public bool IsValid(out List<ValidationResult> errors)
        {
            var ctx = new ValidationContext(this);
            errors = new List<ValidationResult>();

            return Validator.TryValidateObject(this, ctx, errors, true);
        }
        
        
        
    }


}
