using Application.Users.Cmds;
using Application.Users.Queries;
using Application.Users.Vms;
using Microsoft.AspNetCore.Mvc;

namespace WebUi.Controllers;

public class UsersController : BaseController
{

    [HttpGet]
    [ProducesResponseType(typeof(List<UserVm>), 200)]
    public async Task<IActionResult> GetUsers()
    {
        var result = await Mediator.Send(new GetUsersQuery());
        return Ok(result);
    }

    [HttpGet("/{id:long}")]
    [ProducesResponseType(typeof(UserVm), 200)]
    public async Task<IActionResult> GetUsers([FromRoute] long id)
    {
        var result = await Mediator.Send(new GetUserByIdQuery() {Id = id});
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(long), 200)]
    public async Task<IActionResult> EditUserCmd([FromBody] EditUserCmd query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }


}