using MediatR;

namespace SimpleTransfer.Application.Identity.UseCases.UpdateUser.Commands;

public record UpdateUserCommand(string Id, string Name, string Email) 
    : IRequest<object>;