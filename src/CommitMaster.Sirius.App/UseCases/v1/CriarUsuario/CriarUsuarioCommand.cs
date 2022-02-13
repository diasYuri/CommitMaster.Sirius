using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CommitMaster.Sirius.App.Responses.v1;
using CommitMaster.Sirius.Domain.Contracts;
using MediatR;

namespace CommitMaster.Sirius.App.UseCases.v1.CriarUsuario
{
    public class CriarUsuarioCommand : IRequest<HandlerResponse<CriarUsuarioCommandResponse>>, ICommandValidable
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataCriacao { get; set; }
        public Guid AlunoId { get; set; }

        public bool IsValid(out List<ValidationResult> errors)
        {
            throw new System.NotImplementedException();
        }
    }
}
