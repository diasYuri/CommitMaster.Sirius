using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommitMaster.Sirius.Domain.UseCases.Autorization;
using CommitMaster.Sirius.Infra.Autentication;
using CommitMaster.Sirius.Infra.Criptografia.v1;
using CommitMaster.Sirius.Infra.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommitMaster.Sirius.App.UseCases.v1.LoginUsuario
{
    public class LoginUsuarioCommandHandler :
        HandlerResponseBase,
        IRequestHandler<LoginUsuarioCommand, HandlerResponse<LoginUsuarioCommandResponse>>
    {
        private readonly SiriusAppContext _dbcontext;
        private readonly IPasswordEncrypt _passwordEncrypt;
        private readonly ITokenService _tokenService;

        public LoginUsuarioCommandHandler(SiriusAppContext dbcontext, IPasswordEncrypt passwordEncrypt, ITokenService tokenService)
        {
            _dbcontext = dbcontext;
            _passwordEncrypt = passwordEncrypt;
            _tokenService = tokenService;
        }

        public async Task<HandlerResponse<LoginUsuarioCommandResponse>> Handle(LoginUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _dbcontext.Usuarios
                .Include(u => u.Aluno)
                .ThenInclude(a => a.Assinatura)
                .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);


            if (usuario == null)
            {
                return ErroCommand<LoginUsuarioCommandResponse>("Usuário ou Senha inválidos",
                    "Usuário ou Senha inválidos");
            }

            var validPassword = _passwordEncrypt.VerifyPassword(usuario.Senha, request.Senha);
            if (!validPassword)
            {
                return ErroCommand<LoginUsuarioCommandResponse>("Usuário ou Senha inválidos",
                    "Usuário ou Senha inválidos");
            }


            var validadeAssinatura = usuario.ValidaAssinatura();

            string token;
            if (validadeAssinatura == RolesEnum.AssinaturaValida)
            {
                token = _tokenService.CreateToken(usuario.AlunoId, usuario.Email, RolesEnum.AssinaturaValida.ToString());
            }
            else
            {
                token = _tokenService.CreateToken(usuario.AlunoId, usuario.Email, RolesEnum.SemAssinatura.ToString());
            }

            var response = new LoginUsuarioCommandResponse
            {
                Sucesso = true,
                Token = token
            };

            return Sucesso(response);
        }
    }
}
