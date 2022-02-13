using System;
using System.Threading.Tasks;
using CommitMaster.Sirius.App.UseCases.v1.LoginUsuario;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CommitMaster.Sirius.Api.Controllers.v1;


[ApiController]
[Route("api/v1/usuario")]
public class UsuarioController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsuarioController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> AssinarPlano([FromBody] LoginUsuarioCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.Success) {
            return StatusCode((int)result.StatusCode,result.Data);
        }

        return StatusCode((int)result.StatusCode,result.Errors);
    }

}
