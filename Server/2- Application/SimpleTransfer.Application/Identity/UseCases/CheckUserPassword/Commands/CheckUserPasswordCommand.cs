using MediatR;

namespace SimpleTransfer.Application.Identity.UseCases.CheckUserPassword.Commands;

public record CheckUserPasswordCommand(string UserName, string DocumentNumber, string Email, string Password)
    : IRequest<object>;
