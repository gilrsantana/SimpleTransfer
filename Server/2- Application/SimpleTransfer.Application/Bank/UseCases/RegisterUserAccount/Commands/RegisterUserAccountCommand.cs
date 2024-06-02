using MediatR;

namespace SimpleTransfer.Application.Bank.UseCases.RegisterUserAccount.Commands;

public record RegisterUserAccountCommand(string UserId) : IRequest<object>;