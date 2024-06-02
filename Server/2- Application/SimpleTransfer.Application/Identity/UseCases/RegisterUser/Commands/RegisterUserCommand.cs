using MediatR;

namespace SimpleTransfer.Application.Identity.UseCases.RegisterUser.Commands;

public record RegisterUserCommand (string Username, string Email, string Password, string Document) 
    : IRequest<object>;