using System;
using System.Threading.Tasks;
using CommitMaster.Sirius.Api.Services;
using CommitMaster.Sirius.App.UseCases.v1.AssinarPlano;
using CommitMaster.Sirius.App.UseCases.v1.CriarAluno;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommitMaster.Sirius.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/aluno")]
    public class AlunoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserAccessor _userAccessor;

        public AlunoController(IMediator mediator, UserAccessor userAccessor)
        {
            _mediator = mediator;
            _userAccessor = userAccessor;
        }

        [HttpPost()]
        public async Task<IActionResult> CriarAluno([FromBody] CriarAlunoCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return StatusCode((int)result.StatusCode, result.Data);
            }

            return StatusCode((int)result.StatusCode, result.Errors);
        }


        [Authorize]
        [HttpPost("assinar")]
        public async Task<IActionResult> AssinarPlano([FromBody] AssinarPlanoCommand command)
        {
            command.AlunoId = _userAccessor.GetUserId();
            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return StatusCode((int)result.StatusCode, result.Data);
            }

            return StatusCode((int)result.StatusCode, result.Errors);
        }


    }
}
