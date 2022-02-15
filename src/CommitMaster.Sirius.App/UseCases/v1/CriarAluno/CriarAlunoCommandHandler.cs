using System;
using System.Threading;
using System.Threading.Tasks;
using CommitMaster.Contracts.Events.v1;
using CommitMaster.Sirius.App.UseCases.v1.CriarUsuario;
using CommitMaster.Sirius.Domain.Entities;
using CommitMaster.Sirius.Infra.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommitMaster.Sirius.App.UseCases.v1.CriarAluno
{
    public class CriarAlunoCommandHandler :
        HandlerResponseBase,
        IRequestHandler<CriarAlunoCommand, HandlerResponse<CriarAlunoCommandResponse>>
    {
        private readonly SiriusAppContext _appContext;
        private readonly IMediator _mediator;

        public CriarAlunoCommandHandler(SiriusAppContext appContext, IMediator mediator)
        {
            _appContext = appContext;
            _mediator = mediator;
        }

        public async Task<HandlerResponse<CriarAlunoCommandResponse>> Handle(CriarAlunoCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid(out var errors)) {
                return FailValidation<CriarAlunoCommandResponse>(errors);
            }

            var alunoExist = await _appContext
                .Alunos
                .AnyAsync(a => a.Email == request.Email, CancellationToken.None);

            if (alunoExist) {
                return ErroCommand<CriarAlunoCommandResponse>("Erro", "Já existe uma aluno com esse email");
            }
            
            var aluno = new Aluno(
                request.Nome,
                request.Email,
                request.DataAniversario, 
                request.Cpf,
                request.Telefone
                );

            _appContext.Alunos.Add(aluno);
            var sucesso = await _appContext.SaveChangesAsync(cancellationToken) > 0;

            if (!sucesso) {
                return ErroInterno<CriarAlunoCommandResponse>();
            }
            
            
            var command = new CriarUsuarioCommand {
                Nome = request.Nome,
                Senha = request.Senha,
                AlunoId = aluno.Id,
                Email = request.Email,
                DataCriacao = DateTime.UtcNow
            };

            _ = await _mediator.Send(command, cancellationToken);
            
            
            //Notify
            var alunoEvent = new AlunoCriadoEvent {
                Nome = request.Nome,
                Cpf = request.Cpf,
                DataAniversario = request.DataAniversario,
                Email = request.Email,
                Telefone = request.Telefone,
                DataCriacao = DateTime.UtcNow
            };

            await _mediator.Publish(alunoEvent, cancellationToken);

            return SucessoCriado(new CriarAlunoCommandResponse{ AlunoId = aluno.Id});
        }
        
        

    }
}
