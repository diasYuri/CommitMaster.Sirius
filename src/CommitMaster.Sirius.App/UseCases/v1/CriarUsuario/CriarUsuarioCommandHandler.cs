using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommitMaster.Sirius.Domain.Entities;
using CommitMaster.Sirius.Infra.Criptografia.v1;
using CommitMaster.Sirius.Infra.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommitMaster.Sirius.App.UseCases.v1.CriarUsuario
{
    public class CriarUsuarioCommandHandler :
        HandlerResponseBase,
        IRequestHandler<CriarUsuarioCommand, HandlerResponse<CriarUsuarioCommandResponse>>
    {
        private readonly IPasswordEncrypt _passwordEncrypt;
        private readonly SiriusAppContext _dbcontext;

        public CriarUsuarioCommandHandler(IPasswordEncrypt passwordEncrypt, SiriusAppContext dbcontext)
        {
            _passwordEncrypt = passwordEncrypt;
            _dbcontext = dbcontext;
        }


        public async Task<HandlerResponse<CriarUsuarioCommandResponse>> Handle(CriarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = new Usuario(
                request.Email,
                _passwordEncrypt.PasswordHash(request.Senha),
                request.AlunoId
                );

            var userExiste = await _dbcontext.Usuarios.AnyAsync(u => u.Email == request.Email, cancellationToken: cancellationToken);

            if (userExiste) {
                return ErroCommand<CriarUsuarioCommandResponse>("400","Usuário já existe");
            }


            _dbcontext.Usuarios.Add(usuario);
            await _dbcontext.SaveChangesAsync(cancellationToken);

            return SucessoCriado<CriarUsuarioCommandResponse>(new ());
        }
    }

}
