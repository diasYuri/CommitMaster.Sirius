using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CommitMaster.Sirius.App.Responses.v1;
using CommitMaster.Sirius.Domain.Contracts;
using MediatR;

namespace CommitMaster.Sirius.App.UseCases.v1.LoginUsuario
{
    public class LoginUsuarioCommand : IRequest<HandlerResponse<LoginUsuarioCommandResponse>>, ICommandValidable
    {
        public string Senha { get; set; }
        public string Email { get; set; }

        public bool IsValid(out List<ValidationResult> errors)
        {
            throw new System.NotImplementedException();
        }
    }
}
