using System;
using System.Threading.Tasks;
using CommitMaster.Sirius.App.Commands.v1.AlunoUseCases;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CommitMaster.Sirius.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/aluno")]
    public class AlunoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AlunoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost()]
        public async Task<IActionResult> CriarAluno([FromBody] CriarAlunoCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.Success) {
                return StatusCode((int)result.StatusCode,result.Data);
            }

            return StatusCode((int)result.StatusCode,result.Errors);
        }
        
        [HttpPost("{id:guid}/assinar")]
        public async Task<IActionResult> AssinarPlano([FromBody] AssinarPlanoCommand command, Guid id)
        {
            command.AlunoId = id;
            var result = await _mediator.Send(command);

            if (result.Success) {
                return StatusCode((int)result.StatusCode,result.Data);
            }

            return StatusCode((int)result.StatusCode,result.Errors);
        }
    }
}
