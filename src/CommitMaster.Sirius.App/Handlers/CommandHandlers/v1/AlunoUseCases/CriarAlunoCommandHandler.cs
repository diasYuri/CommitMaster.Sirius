using System;
using System.Threading;
using System.Threading.Tasks;
using CommitMaster.Sirius.App.Commands.v1.AlunoUseCases;
using CommitMaster.Sirius.App.Events.v1.AlunoUseCases;
using CommitMaster.Sirius.App.Responses.v1;
using CommitMaster.Sirius.App.Responses.v1.AlunoUseCases;
using CommitMaster.Sirius.Domain.Entities;
using CommitMaster.Sirius.Infra.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommitMaster.Sirius.App.Handlers.CommandHandlers.v1.AlunoUseCases
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
            
            //TODO - Criar usuário
            
            
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
