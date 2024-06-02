using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleTransfer.API.Notifications;
using SimpleTransfer.API.ViewModels;
using SimpleTransfer.Application.Identity.UseCases.RegisterUser.Commands;
using SimpleTransfer.Application.Identity.UseCases.RegisterUser.Notifications;
using SimpleTransfer.Application.Notifications;

namespace SimpleTransfer.API.Controllers;
[ApiController]
[Route("[controller]")]
public class IdentityController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResultViewModel<RegisteredUserNotification>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ResultViewModel<object>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ResultViewModel<object>))]
    public async Task<IActionResult> Index(RegisterUserViewModel model)
    {
        try
        {
            var command = new RegisterUserCommand(model.Username, model.Email, model.Password, model.Document);
            var result = await mediator.Send(command);
            return result is ErrorNotification notification
                ? BadRequest(new ResultViewModel<object>(notification.Messages.Select(x => 
                    new MessageModel(x, EMessageType.Error)).ToList()))
                : Ok(new ResultViewModel<RegisteredUserNotification>((RegisteredUserNotification)result));
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, 
                new ResultViewModel<object>(
                    new MessageModel("Erro interno ao registrar novo usuário", EMessageType.CriticalError)));
        }
    }
}