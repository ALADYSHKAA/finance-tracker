using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebUi.Controllers;

[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(typeof(InternalServerErrorDto), 500)]
public abstract class BaseController : ControllerBase
{
    private IMediator _mediator;

    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}

public class InternalServerErrorDto
{
    public string ActionId { get; set; }
    public string Error { get; set; }
}